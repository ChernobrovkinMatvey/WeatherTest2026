using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Местоположение из ответа WeatherAPI
/// </summary>
public class LocationRawResponse
{
    /// <summary>Название города.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>Регион или область.</summary>
    [JsonPropertyName("region")]
    public string Region { get; set; } = string.Empty;

    /// <summary>Название страны.</summary>
    [JsonPropertyName("country")]
    public string Country { get; set; } = string.Empty;

    /// <summary>Широта.</summary>
    [JsonPropertyName("lat")]
    public double Lat { get; set; }

    /// <summary>Долгота.</summary>
    [JsonPropertyName("lon")]
    public double Lon { get; set; }

    /// <summary>Идентификатор часового пояса.</summary>
    [JsonPropertyName("tz_id")]
    public string TzId { get; set; } = string.Empty;

    /// <summary>Локальное время в формате yyyy-MM-dd HH:mm.</summary>
    [JsonPropertyName("localtime")]
    public string Localtime { get; set; } = string.Empty;
}