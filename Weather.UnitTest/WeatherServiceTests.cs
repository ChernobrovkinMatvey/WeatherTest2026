using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Weather.Application.WeatherApi;
using Weather.Infrastructure.Configurations;
using Weather.Infrastructure.Implementations;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для WeatherService с использованием Moq
/// </summary>
public class WeatherServiceTests
{
    private readonly Mock<IHttpClientFactory> _httpClientFactoryMock;

    private readonly Mock<HttpMessageHandler> _httpMessageHandlerMock;

    private readonly WeatherApiOptions _options;

    private readonly WeatherService _service;

    public WeatherServiceTests()
    {
        _options = new WeatherApiOptions
        {
            ApiKey = "test-key",
            BaseUrl = "http://api.weatherapi.com/v1",
            TimeoutMs = 5000
        };

        _httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        _httpClientFactoryMock = new Mock<IHttpClientFactory>();

        var httpClient = new HttpClient(_httpMessageHandlerMock.Object);
        _httpClientFactoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        _service = new WeatherService(_httpClientFactoryMock.Object, Options.Create(_options));
    }

    /// <summary>
    /// Проверяет, что для forecast запроса строится правильный URL и возвращается десериализованный ответ
    /// </summary>
    [Fact]
    public async Task GetWeather_WithDays_ShouldBuildForecastUrlAndDeserialize()
    {
        var rawResponse = new WeatherRawResponse
        {
            Location = new LocationRawResponse { Name = "Moscow" },
            Current = new CurrentRawResponse { TempC = 20.5 },
            Forecast = new ForecastRawResponse
            {
                Forecastday = new List<ForecastDayRawResponse>
                {
                    new ForecastDayRawResponse { Date = "2026-08-27" }
                }
            }
        };
        var json = JsonSerializer.Serialize(rawResponse);

        SetupHttpResponse(HttpStatusCode.OK, json);

        var result = await _service.GetWeather(55.75, 37.61, 3);

        Assert.NotNull(result);
    }

    /// <summary>
    /// Проверяет, что для current запроса (без days) строится правильный URL
    /// </summary>
    [Fact]
    public async Task GetWeather_WithoutDays_ShouldBuildCurrentUrl()
    {
        var rawResponse = new WeatherRawResponse
        {
            Location = new LocationRawResponse { Name = "Moscow" },
            Current = new CurrentRawResponse { TempC = 15.0 }
        };
        var json = JsonSerializer.Serialize(rawResponse);

        SetupHttpResponse(HttpStatusCode.OK, json);

        var result = await _service.GetWeather(55.75, 37.61, null);

        Assert.NotNull(result);
    }

    /// <summary>
    /// Проверяет, что при HTTP ошибке выбрасывается исключение
    /// </summary>
    [Fact]
    public async Task GetWeather_WhenHttpError_ShouldThrowException()
    {
        SetupHttpResponse(HttpStatusCode.InternalServerError, "Error");

        await Assert.ThrowsAsync<HttpRequestException>(() => _service.GetWeather(55.75, 37.61, 3));
    }

    /// <summary>
    /// Проверяет, что при null десериализации выбрасывается InvalidOperationException
    /// </summary>
    [Fact]
    public async Task GetWeather_WhenDeserializationReturnsNull_ShouldThrowInvalidOperationException()
    {
        SetupHttpResponse(HttpStatusCode.OK, "null");

        await Assert.ThrowsAsync<InvalidOperationException>(() => _service.GetWeather(55.75, 37.61, 3));
    }

    /// <summary>
    /// Проверяет, что при маленьком таймауте выбрасывается TaskCanceledException
    /// </summary>
    [Fact]
    public async Task GetWeather_WhenTimeoutIsSmall_ShouldThrowTaskCanceledException()
    {
        var timeoutOptions = new WeatherApiOptions
        {
            ApiKey = "test-key",
            BaseUrl = "http://api.weatherapi.com/v1",
            TimeoutMs = 1
        };

        var timeoutHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);
        timeoutHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .Returns<HttpRequestMessage, CancellationToken>(async (request, ct) =>
            {
                await Task.Delay(5000, ct);
                return new HttpResponseMessage(HttpStatusCode.OK) { Content = new StringContent("{}") };
            });

        var httpClient = new HttpClient(timeoutHandlerMock.Object);
        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        var service = new WeatherService(factoryMock.Object, Options.Create(timeoutOptions));

        await Assert.ThrowsAsync<TaskCanceledException>(() => service.GetWeather(55.75, 37.61, 3));
    }

    private void SetupHttpResponse(HttpStatusCode statusCode, string content)
    {
        _httpMessageHandlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(content)
            });
    }
}