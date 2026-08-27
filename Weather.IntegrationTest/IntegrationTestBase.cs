using System.Net;
using System.Text.Json;
using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Application.WeatherApi;
using Weather.Domain.Models;
using Weather.Infrastructure.Configurations;
using Weather.Infrastructure.Handlers;
using Weather.Infrastructure.Implementations;
using Weather.Infrastructure.MappingProfiles;
using Weather.Infrastructure.Validators;

namespace Weather.IntegrationTest;

/// <summary>
/// Базовый класс для интеграционных тестов, предоставляющий общие зависимости
/// </summary>
public class IntegrationTestBase
{
    /// <summary>
    /// Создаёт настроенный IMapper с обоими профилями маппинга
    /// </summary>
    protected static IMapper CreateMapper()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WeatherApiMappingProfile>();
            cfg.AddProfile<ToContractsMappingProfile>();
        }, NullLoggerFactory.Instance);

        return config.CreateMapper();
    }

    /// <summary>
    /// Создаёт Mock<IHttpClientFactory> с заданным ответом JSON
    /// </summary>
    protected static Mock<IHttpClientFactory> CreateHttpClientFactoryMock(HttpStatusCode statusCode, string jsonContent)
    {
        var handlerMock = new Mock<HttpMessageHandler>(MockBehavior.Strict);

        handlerMock
            .Protected()
            .Setup<Task<HttpResponseMessage>>(
                "SendAsync",
                ItExpr.IsAny<HttpRequestMessage>(),
                ItExpr.IsAny<CancellationToken>())
            .ReturnsAsync(new HttpResponseMessage
            {
                StatusCode = statusCode,
                Content = new StringContent(jsonContent)
            });

        var httpClient = new HttpClient(handlerMock.Object);
        var factoryMock = new Mock<IHttpClientFactory>();
        factoryMock.Setup(f => f.CreateClient(It.IsAny<string>())).Returns(httpClient);

        return factoryMock;
    }

    /// <summary>
    /// Создаёт WeatherService с mocked HTTP клиентом
    /// </summary>
    protected static WeatherService CreateWeatherService(IHttpClientFactory httpClientFactory)
    {
        var options = Options.Create(new WeatherApiOptions
        {
            ApiKey = "test-key",
            BaseUrl = "http://api.weatherapi.com/v1",
            TimeoutMs = 5000
        });

        return new WeatherService(httpClientFactory, options);
    }

    /// <summary>
    /// Создаёт типовой WeatherRawResponse для тестов
    /// </summary>
    protected static string CreateSampleWeatherJson()
    {
        var rawResponse = new WeatherRawResponse
        {
            Location = new LocationRawResponse
            {
                Name = "Moscow",
                Region = "Moscow City",
                Country = "Russia",
                Localtime = "2026-08-27 14:30",
                Lat = 55.75,
                Lon = 37.61,
                TzId = "Europe/Moscow"
            },
            Current = new CurrentRawResponse
            {
                TempC = 22.5,
                FeelslikeC = 20.0,
                Humidity = 65,
                WindKph = 15.0,
                Uv = 3.5,
                IsDay = 1,
                LastUpdated = "2026-08-27 14:15",
                Condition = new ConditionRawResponse { Text = "Sunny", Icon = "//cdn.weatherapi.com/icon.png", Code = 1000 }
            },
            Forecast = new ForecastRawResponse
            {
                Forecastday = new List<ForecastDayRawResponse>
                {
                    new ForecastDayRawResponse
                    {
                        Date = "2026-08-27",
                        Day = new DayRawResponse
                        {
                            AvgtempC = 22.0,
                            MaxWindKph = 18.0,
                            Condition = new ConditionRawResponse { Text = "Sunny", Icon = "//cdn.weatherapi.com/icon.png", Code = 1000 }
                        },
                        Astro = new AstroRawResponse { Sunrise = "05:30 AM", Sunset = "08:15 PM" },
                        Hour = new List<HourRawResponse>
                        {
                            new HourRawResponse { Time = "2026-08-27 12:00", TempC = 22.0, ChanceOfRain = 10, IsDay = 1 }
                        }
                    }
                }
            }
        };

        return JsonSerializer.Serialize(rawResponse);
    }
}