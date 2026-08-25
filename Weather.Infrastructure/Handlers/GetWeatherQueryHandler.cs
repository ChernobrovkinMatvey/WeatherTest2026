using AutoMapper;
using FluentValidation;
using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Models;

namespace Weather.Infrastructure.Handlers;

public class GetWeatherQueryHandler : IRequestHandler<GetWeatherQuery, WeatherResponse>
{
    private readonly IWeatherService _weatherService;

    private readonly IMapper _mapper;

    private readonly IValidator<GetWeatherQuery> _validator;

    public GetWeatherQueryHandler(
        IWeatherService weatherService,
        IMapper mapper,
        IValidator<GetWeatherQuery> validator)
    {
        _weatherService = weatherService;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<WeatherResponse> Handle(GetWeatherQuery request, CancellationToken cancellationToken)
    {
        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var rawResponse = await _weatherService.GetWeather(request.Lat, request.Lon, request.Days, cancellationToken);

        return _mapper.Map<WeatherResponse>(rawResponse);
    }
}