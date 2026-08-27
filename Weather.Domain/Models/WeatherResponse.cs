namespace Weather.Domain.Models;

/// <summary>
/// Полный ответ с погодными данными: местоположение, текущая погода и прогноз
/// </summary>
public class WeatherResponse
{
    /// <summary>Название города.</summary>
    public string City { get; set; } = string.Empty;

    /// <summary>Регион или область.</summary>
    public string Region { get; set; } = string.Empty;

    /// <summary>Название страны.</summary>
    public string Country { get; set; } = string.Empty;

    /// <summary>Локальное время в формате yyyy-MM-dd HH:mm.</summary>
    public string LocalTime { get; set; } = string.Empty;

    /// <summary>Текущая погода.</summary>
    public CurrentWeather? Current { get; set; }

    /// <summary>Прогноз погоды по дням.</summary>
    public ICollection<ForecastDay>? Forecastdays { get; set; }
}