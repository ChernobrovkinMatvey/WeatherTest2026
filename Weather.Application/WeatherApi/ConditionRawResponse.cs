using System.Text.Json.Serialization;

namespace Weather.Application.WeatherApi;

/// <summary>
/// Погодное условие из ответа WeatherAPI (текст, иконка, код)
/// </summary>
public class ConditionRawResponse
{
    /// <summary>Текстовое описание погоды.</summary>
    [JsonPropertyName("text")]
    public string Text { get; set; } = string.Empty;

    /// <summary>URL иконки погоды.</summary>
    [JsonPropertyName("icon")]
    public string Icon { get; set; } = string.Empty;

    /// <summary>Числовой код погодного условия.</summary>
    [JsonPropertyName("code")]
    public int Code { get; set; }
}