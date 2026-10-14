using AutoMapper;
using Weather.Application.Contracts;
using Weather.Application.DTOs;
using Weather.Domain.Entities;
using Weather.Domain.Models;

namespace Weather.Infrastructure.MappingProfiles;

/// <summary>
/// Профиль маппинга моделей в контракты для внешнего API
/// </summary>
public class ToContractsMappingProfile : Profile
{
    public ToContractsMappingProfile()
    {
        CreateMap<WeatherCondition, WeatherConditionContract>();

        CreateMap<CurrentWeather, CurrentWeatherContract>()
            .ForMember(d => d.WindMps, o => o.MapFrom(s => Math.Round(s.WindKph / 3.6, 1)));

        CreateMap<Hour, HourContract>();

        CreateMap<ForecastDay, ForecastDayContract>()
            .ForMember(d => d.MaxWindMps, o => o.MapFrom(s => Math.Round(s.MaxWindKph / 3.6, 1)));

        CreateMap<WeatherResponse, WeatherForecastContract>();
        
        CreateMap<CityOfSubject, CityOfSubjectContract>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()));

        CreateMap<CityOfSubjectCreateContract, CityOfSubjectPushDto>();
        
        CreateMap<CityOfSubjectContract, CityOfSubjectPushDto>();

        CreateMap<CityOfSubjectPushDto, CityOfSubject>()
            .ForMember(d => d.Id, opt => opt.Ignore());
    }
}