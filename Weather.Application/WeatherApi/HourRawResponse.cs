using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Почасовой прогноз из ответа WeatherAPI
/// </summary>
public class HourRawResponse
{
    /// <summary>Время в формате yyyy-MM-dd HH:mm.</summary>
    [JsonPropertyName("time")]
    public string Time { get; set; } = string.Empty;

    /// <summary>Температура в градусах Цельсия.</summary>
    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }

    /// <summary>Погодное условие.</summary>
    [JsonPropertyName("condition")]
    public ConditionRawResponse? Condition { get; set; }

    /// <summary>Вероятность дождя в процентах.</summary>
    [JsonPropertyName("chance_of_rain")]
    public int ChanceOfRain { get; set; }

    /// <summary>Признак дня (1 — день, 0 — ночь).</summary>
    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }
}