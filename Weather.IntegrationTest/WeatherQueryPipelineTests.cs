using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Application.WeatherApi;
using Weather.Domain.Models;
using Weather.Infrastructure.Handlers;
using Weather.Infrastructure.Implementations;
using Weather.Infrastructure.MappingProfiles;
using Weather.Infrastructure.Validators;

namespace Weather.IntegrationTest;

/// <summary>
/// Интеграционный тест полного pipeline: GetWeatherQuery -> валидатор -> хендлер -> сервис -> маппинг -> ответ
/// </summary>
public class WeatherQueryPipelineTests : IntegrationTestBase
{
    /// <summary>
    /// Проверяет полный конвейер обработки запроса погоды с 3-дневным прогнозом:
    /// валидация -> вызов WeatherService (mocked HTTP) -> маппинг WeatherRawResponse -> WeatherResponse
    /// </summary>
    [Fact]
    public async Task GetWeatherQuery_FullPipeline_ShouldReturnMappedWeatherResponse()
    {
        var jsonContent = CreateSampleWeatherJson();
        var httpClientFactoryMock = CreateHttpClientFactoryMock(System.Net.HttpStatusCode.OK, jsonContent);
        var weatherService = CreateWeatherService(httpClientFactoryMock.Object);
        var mapper = CreateMapper();
        var validator = new GetWeatherQueryValidator();
        var handler = new GetWeatherQueryHandler(weatherService, mapper, validator);

        var query = new GetWeatherQuery
        {
            Lat = 55.75,
            Lon = 37.61,
            Days = 3
        };

        var result = await handler.Handle(query, CancellationToken.None);

        Assert.NotNull(result);
        Assert.Equal("Moscow", result.City);
        Assert.Equal("Moscow City", result.Region);
        Assert.Equal("Russia", result.Country);
        Assert.Equal("2026-08-27 14:30", result.LocalTime);

        Assert.NotNull(result.Current);
        Assert.Equal(22.5, result.Current.TempC);
        Assert.Equal(20.0, result.Current.FeelsLikeC);
        Assert.Equal(65, result.Current.Humidity);
        Assert.Equal("Sunny", result.Current.Condition?.Text);

        Assert.NotNull(result.Forecastdays);
        var day = Assert.Single(result.Forecastdays);
        Assert.Equal("2026-08-27", day.Date);
        Assert.Equal(22.0, day.AvgtempC);
        Assert.Equal(18.0, day.MaxWindKph);
        Assert.Equal("05:30", day.Sunrise);
        Assert.Equal("08:15", day.Sunset);
        Assert.NotNull(day.Hours);
        var hour = Assert.Single(day.Hours);
        Assert.Equal(22.0, hour.TempC);
    }

    /// <summary>
    /// Проверяет, что невалидный запрос (Days=1) пробрасывает ValidationException через весь pipeline
    /// </summary>
    [Fact]
    public async Task GetWeatherQuery_WithInvalidDays_ShouldThrowValidationException()
    {
        var httpClientFactoryMock = CreateHttpClientFactoryMock(System.Net.HttpStatusCode.OK, "{}");
        var weatherService = CreateWeatherService(httpClientFactoryMock.Object);
        var mapper = CreateMapper();
        var validator = new GetWeatherQueryValidator();
        var handler = new GetWeatherQueryHandler(weatherService, mapper, validator);

        var query = new GetWeatherQuery
        {
            Lat = 55.75,
            Lon = 37.61,
            Days = 1
        };

        await Assert.ThrowsAsync<FluentValidation.ValidationException>(() =>
            handler.Handle(query, CancellationToken.None));
    }

    /// <summary>
    /// Проверяет, что при HTTP ошибке от WeatherAPI pipeline пробрасывает исключение
    /// </summary>
    [Fact]
    public async Task GetWeatherQuery_WhenApiReturnsError_ShouldThrowHttpRequestException()
    {
        var httpClientFactoryMock = CreateHttpClientFactoryMock(System.Net.HttpStatusCode.InternalServerError, "Error");
        var weatherService = CreateWeatherService(httpClientFactoryMock.Object);
        var mapper = CreateMapper();
        var validator = new GetWeatherQueryValidator();
        var handler = new GetWeatherQueryHandler(weatherService, mapper, validator);

        var query = new GetWeatherQuery
        {
            Lat = 55.75,
            Lon = 37.61,
            Days = 3
        };

        await Assert.ThrowsAsync<HttpRequestException>(() =>
            handler.Handle(query, CancellationToken.None));
    }
}