namespace Weather.Application.Contracts;

/// <summary>
/// Контракт погодного условия для внешнего API.
/// </summary>
public class WeatherConditionContract
{
    /// <summary>Текстовое описание погоды (например, "Солнечно").</summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>URL иконки погоды.</summary>
    public string Icon { get; set; } = string.Empty;
}