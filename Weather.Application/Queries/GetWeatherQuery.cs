using MediatR;
using Weather.Domain.Models;

namespace Weather.Application.Queries;

/// <summary>
/// CQRS-запрос на получение погоды для указанных координат.
/// </summary>
public class GetWeatherQuery : IRequest<WeatherResponse>
{
    /// <summary>Широта.</summary>
    public double Lat { get; set; }

    /// <summary>Долгота.</summary>
    public double Lon { get; set; }

    /// <summary>Количество дней прогноза.</summary>
    public int? Days { get; set; }
}