using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Application.WeatherApi;
using Weather.Domain.Models;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для WeatherApiMappingProfile: маппинг сырых ответов WeatherAPI в модели приложения
/// </summary>
public class WeatherApiMappingProfileTests
{
    private readonly IMapper _mapper;

    public WeatherApiMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<WeatherApiMappingProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Проверяет, что при null Forecast поле Forecastdays является пустой коллекцией
    /// </summary>
    [Fact]
    public void Map_WhenForecastIsNull_ShouldMapForecastdaysAsEmptyCollection()
    {
        var source = new WeatherRawResponse
        {
            Location = new LocationRawResponse()
        };

        var result = _mapper.Map<WeatherResponse>(source);

        Assert.NotNull(result.Forecastdays);
        Assert.Empty(result.Forecastdays);
    }
}