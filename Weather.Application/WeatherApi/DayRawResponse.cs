using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Дневные показатели прогноза из ответа WeatherAPI.
/// </summary>
public class DayRawResponse
{
    /// <summary>Максимальная температура в градусах Цельсия.</summary>
    [JsonPropertyName("maxtemp_c")]
    public double MaxtempC { get; set; }

    /// <summary>Минимальная температура в градусах Цельсия.</summary>
    [JsonPropertyName("mintemp_c")]
    public double MintempC { get; set; }

    /// <summary>Погодное условие.</summary>
    [JsonPropertyName("condition")]
    public ConditionRawResponse? Condition { get; set; }
}