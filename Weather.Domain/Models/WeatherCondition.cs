namespace Weather.Domain.Models;

/// <summary>
/// Погодное условие: текстовое описание и иконка.
/// </summary>
public class WeatherCondition
{
    /// <summary>Текстовое описание погоды (например, "Солнечно").</summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>URL иконки погоды.</summary>
    public string Icon { get; set; } = string.Empty;
}