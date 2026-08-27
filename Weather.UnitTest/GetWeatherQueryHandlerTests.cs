using AutoMapper;
using FluentValidation;
using Moq;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Application.WeatherApi;
using Weather.Domain.Models;
using Weather.Infrastructure.Handlers;
using Weather.Infrastructure.Validators;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для GetWeatherQueryHandler с использованием Moq
/// </summary>
public class GetWeatherQueryHandlerTests
{
    private readonly Mock<IWeatherService> _weatherServiceMock;

    private readonly Mock<IMapper> _mapperMock;

    private readonly GetWeatherQueryValidator _validator;

    private readonly GetWeatherQueryHandler _handler;

    public GetWeatherQueryHandlerTests()
    {
        _weatherServiceMock = new Mock<IWeatherService>();
        _mapperMock = new Mock<IMapper>();
        _validator = new GetWeatherQueryValidator();
        _handler = new GetWeatherQueryHandler(
            _weatherServiceMock.Object,
            _mapperMock.Object,
            _validator);
    }

    /// <summary>
    /// Проверяет, что Handle вызывает сервис погоды и маппинг при успешной валидации
    /// </summary>
    [Fact]
    public async Task Handle_WhenCalled_CallsServiceAndMapper()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = null };
        var rawResponse = new WeatherRawResponse();
        var weatherResponse = new WeatherResponse();

        _weatherServiceMock
            .Setup(s => s.GetWeather(query.Lat, query.Lon, query.Days, It.IsAny<CancellationToken>()))
            .ReturnsAsync(rawResponse);

        _mapperMock
            .Setup(m => m.Map<WeatherResponse>(rawResponse))
            .Returns(weatherResponse);

        var result = await _handler.Handle(query, CancellationToken.None);

        Assert.Same(weatherResponse, result);
        _weatherServiceMock.Verify(s => s.GetWeather(query.Lat, query.Lon, query.Days, It.IsAny<CancellationToken>()), Times.Once);
        _mapperMock.Verify(m => m.Map<WeatherResponse>(rawResponse), Times.Once);
    }

    /// <summary>
    /// Проверяет, что Handle пробрасывает ValidationException при невалидных данных
    /// </summary>
    [Fact]
    public async Task Handle_WhenValidationFails_ThrowsValidationException()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = 1 };

        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(query, CancellationToken.None));

        _weatherServiceMock.Verify(s => s.GetWeather(It.IsAny<double>(), It.IsAny<double>(), It.IsAny<int?>(), It.IsAny<CancellationToken>()), Times.Never);
        _mapperMock.Verify(m => m.Map<WeatherResponse>(It.IsAny<object>()), Times.Never);
    }
}