namespace Weather.Application.Contracts;

/// <summary>
/// Контракт полного ответа с погодными данными для внешнего API
/// </summary>
public class WeatherForecastContract
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
    public CurrentWeatherContract? Current { get; set; }

    /// <summary>Прогноз погоды по дням.</summary>
    public ICollection<ForecastDayContract>? Forecastdays { get; set; }
}