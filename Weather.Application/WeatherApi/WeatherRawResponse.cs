using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Корневой ответ от WeatherAPI (forecast.json)
/// </summary>
public class WeatherRawResponse
{
    /// <summary>Географическое местоположение.</summary>
    [JsonPropertyName("location")]
    public LocationRawResponse? Location { get; set; }

    /// <summary>Текущие погодные условия.</summary>
    [JsonPropertyName("current")]
    public CurrentRawResponse? Current { get; set; }

    /// <summary>Прогноз погоды (forecastday[]).</summary>
    [JsonPropertyName("forecast")]
    public ForecastRawResponse? Forecast { get; set; }
}