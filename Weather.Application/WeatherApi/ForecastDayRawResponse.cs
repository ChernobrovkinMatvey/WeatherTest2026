using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Прогноз на один день из ответа WeatherAPI (дата, день, астрономия, почасовка)
/// </summary>
public class ForecastDayRawResponse
{
    /// <summary>Дата в формате yyyy-MM-dd.</summary>
    [JsonPropertyName("date")]
    public string Date { get; set; } = string.Empty;

    /// <summary>Дневные показатели (температуры, условие).</summary>
    [JsonPropertyName("day")]
    public DayRawResponse? Day { get; set; }

    /// <summary>Астрономические данные (восход, закат).</summary>
    [JsonPropertyName("astro")]
    public AstroRawResponse? Astro { get; set; }

    /// <summary>Почасовой прогноз (24 часа).</summary>
    [JsonPropertyName("hour")]
    public ICollection<HourRawResponse>? Hour { get; set; }
}