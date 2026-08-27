using AutoMapper;
using Weather.Application.WeatherApi;
using Weather.Domain.Models;

namespace Weather.Infrastructure.MappingProfiles;

/// <summary>
/// Профиль маппинга сырых ответов WeatherAPI (RawResponse) в модели приложения
/// </summary>
public class WeatherApiMappingProfile : Profile
{
    public WeatherApiMappingProfile()
    {
        CreateMap<WeatherRawResponse, WeatherResponse>()
            .ForMember(d => d.City, o => o.MapFrom(s => s.Location == null ? "" : s.Location.Name))
            .ForMember(d => d.Region, o => o.MapFrom(s => s.Location == null ? "" : s.Location.Region))
            .ForMember(d => d.Country, o => o.MapFrom(s => s.Location == null ? "" : s.Location.Country))
            .ForMember(d => d.LocalTime, o => o.MapFrom(s => s.Location == null ? "" : s.Location.Localtime))
            .ForMember(d => d.Forecastdays,
            o => o.MapFrom(s => s.Forecast == null ? null : s.Forecast.Forecastday));

        CreateMap<CurrentRawResponse, CurrentWeather>();
        CreateMap<ConditionRawResponse, WeatherCondition>();

        CreateMap<ForecastDayRawResponse, ForecastDay>()
            .ForMember(d => d.AvgtempC, o => o.MapFrom(s => s.Day == null ? 0 : s.Day.AvgtempC))
            .ForMember(d => d.MaxWindKph, o => o.MapFrom(s => s.Day == null ? 0 : s.Day.MaxWindKph))
            .ForMember(d => d.Condition, o => o.MapFrom(s => s.Day == null ? null : s.Day.Condition))
            .ForMember(d => d.Sunrise, o => o.MapFrom(s => s.Astro == null ? "" : s.Astro.Sunrise))
            .ForMember(d => d.Sunset, o => o.MapFrom(s => s.Astro == null ? "" : s.Astro.Sunset))
            .ForMember(d => d.Hours, o => o.MapFrom(s => s.Hour ?? new List<HourRawResponse>()));

        CreateMap<HourRawResponse, Hour>();
    }
}