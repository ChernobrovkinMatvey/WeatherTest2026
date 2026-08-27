using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Application.Contracts;
using Weather.Domain.Entities;
using Weather.Domain.Models;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для ToContractsMappingProfile: маппинг моделей домена в контракты для внешнего API
/// </summary>
public class ToContractsMappingProfileTests
{
    private readonly IMapper _mapper;

    public ToContractsMappingProfileTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ToContractsMappingProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
    }

    /// <summary>
    /// Проверяет маппинг CurrentWeather -> CurrentWeatherContract с конвертацией WindKph в WindMps
    /// </summary>
    [Fact]
    public void Map_CurrentWeather_ShouldConvertWind()
    {
        var source = new CurrentWeather
        {
            TempC = 22.5,
            FeelsLikeC = 20.0,
            Humidity = 65,
            WindKph = 15.0,
            Uv = 3.5,
            LastUpdated = "2026-08-27 14:15",
            Condition = new WeatherCondition { Text = "Sunny", Icon = "//icon.png" }
        };

        var result = _mapper.Map<CurrentWeatherContract>(source);
        
        Assert.Equal(15.0, result.WindKph);
        Assert.Equal(4.2, result.WindMps);
    }

    /// <summary>
    /// Проверяет конвертацию скорости ветра км/ч -> м/с с округлением до 1 знака
    /// </summary>
    [Theory]
    [InlineData(3.6, 1.0)]
    [InlineData(10.0, 2.8)]
    [InlineData(25.0, 6.9)]
    [InlineData(0.0, 0.0)]
    public void Map_CurrentWeather_WindKphToMps_ShouldRoundCorrectly(double windKph, double expectedWindMps)
    {
        var source = new CurrentWeather { WindKph = windKph, LastUpdated = "2026-08-27 14:15" };
        var result = _mapper.Map<CurrentWeatherContract>(source);
        Assert.Equal(expectedWindMps, result.WindMps);
    }

    /// <summary>
    /// Проверяет маппинг ForecastDay -> ForecastDayContract с конвертацией скорости ветра
    /// </summary>
    [Fact]
    public void Map_ForecastDay_ShouldConvertWind()
    {
        var source = new ForecastDay
        {
            Date = "2026-08-27",
            AvgtempC = 22.0,
            MaxWindKph = 18.0,
            Sunrise = "05:30",
            Sunset = "08:15",
            Condition = new WeatherCondition { Text = "Sunny", Icon = "//icon.png" },
            Hours = new List<Hour>
            {
                new Hour { Time = "2026-08-27 12:00", TempC = 22.0, ChanceOfRain = 10 }
            }
        };

        var result = _mapper.Map<ForecastDayContract>(source);
        
        Assert.Equal(18.0, result.MaxWindKph);
        Assert.Equal(5.0, result.MaxWindMps);
    }
}