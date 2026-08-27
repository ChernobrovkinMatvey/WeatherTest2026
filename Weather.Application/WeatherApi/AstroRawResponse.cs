using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Астрономические данные из ответа WeatherAPI (восход, закат)
/// </summary>
public class AstroRawResponse
{
    /// <summary>Время восхода солнца.</summary>
    [JsonPropertyName("sunrise")]
    public string Sunrise { get; set; } = string.Empty;

    /// <summary>Время заката солнца.</summary>
    [JsonPropertyName("sunset")]
    public string Sunset { get; set; } = string.Empty;
}