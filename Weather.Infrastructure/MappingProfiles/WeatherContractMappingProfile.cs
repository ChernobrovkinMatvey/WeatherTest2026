using AutoMapper;
using Weather.Application.Contracts;
using Weather.Domain.Models;

namespace Weather.Infrastructure.MappingProfiles;

/// <summary>
/// Профиль маппинга моделей в контракты для внешнего API.
/// </summary>
public class WeatherContractMappingProfile : Profile
{
    public WeatherContractMappingProfile()
    {
        CreateMap<WeatherCondition, WeatherConditionContract>();

        CreateMap<CurrentWeather, CurrentWeatherContract>();

        CreateMap<Hour, HourContract>();

        CreateMap<ForecastDay, ForecastDayContract>();

        CreateMap<WeatherResponse, WeatherForecastContract>();
    }
}