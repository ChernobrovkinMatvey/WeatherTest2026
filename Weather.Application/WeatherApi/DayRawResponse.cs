using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Дневные показатели прогноза из ответа WeatherAPI
/// </summary>
public class DayRawResponse
{
    /// <summary>Средняя температура в градусах Цельсия.</summary>
    [JsonPropertyName("avgtemp_c")]
    public double AvgtempC { get; set; }

    /// <summary>Максимальная скорость ветра в км/ч.</summary>
    [JsonPropertyName("maxwind_kph")]
    public double MaxWindKph { get; set; }

    /// <summary>Погодное условие.</summary>
    [JsonPropertyName("condition")]
    public ConditionRawResponse? Condition { get; set; }
}