using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Контейнер прогноза с массивом дней из ответа WeatherAPI.
/// </summary>
public class ForecastRawResponse
{
    /// <summary>Массив прогнозов по дням.</summary>
    [JsonPropertyName("forecastday")]
    public ICollection<ForecastDayRawResponse>? Forecastday { get; set; }
}