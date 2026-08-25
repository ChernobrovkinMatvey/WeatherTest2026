namespace Weather.Infrastructure.Configurations;

/// <summary>
/// Настройки для подключения к WeatherAPI.com.
/// </summary>
public class WeatherApiOptions
{
    public const string SectionName = "WeatherApi";

    /// <summary>Ключ API для доступа к WeatherAPI.com.</summary>
    public string ApiKey { get; set; } = string.Empty;

    /// <summary>Базовый URL API (например, http://api.weatherapi.com/v1).</summary>
    public string BaseUrl { get; set; } = string.Empty;
}