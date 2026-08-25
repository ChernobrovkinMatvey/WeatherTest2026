using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Текущие погодные условия из ответа WeatherAPI.
/// </summary>
public class CurrentRawResponse
{
    /// <summary>Дата и время последнего обновления.</summary>
    [JsonPropertyName("last_updated")]
    public string LastUpdated { get; set; } = string.Empty;

    /// <summary>Температура в градусах Цельсия.</summary>
    [JsonPropertyName("temp_c")]
    public double TempC { get; set; }

    /// <summary>Ощущаемая температура в градусах Цельсия.</summary>
    [JsonPropertyName("feelslike_c")]
    public double FeelslikeC { get; set; }

    /// <summary>Относительная влажность в процентах.</summary>
    [JsonPropertyName("humidity")]
    public int Humidity { get; set; }

    /// <summary>Скорость ветра в км/ч.</summary>
    [JsonPropertyName("wind_kph")]
    public double WindKph { get; set; }

    /// <summary>УФ-индекс.</summary>
    [JsonPropertyName("uv")]
    public double Uv { get; set; }

    /// <summary>Признак дня (1 — день, 0 — ночь).</summary>
    [JsonPropertyName("is_day")]
    public int IsDay { get; set; }

    /// <summary>Погодное условие.</summary>
    [JsonPropertyName("condition")]
    public ConditionRawResponse? Condition { get; set; }
}