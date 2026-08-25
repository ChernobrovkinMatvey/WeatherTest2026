namespace Weather.Domain.Models;

/// <summary>
/// Прогноз погоды на один день.
/// </summary>
public class ForecastDay
{
    /// <summary>Дата прогноза в формате yyyy-MM-dd.</summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>Максимальная температура за день в градусах Цельсия.</summary>
    public double MaxtempC { get; set; }

    /// <summary>Минимальная температура за день в градусах Цельсия.</summary>
    public double MintempC { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherCondition? Condition { get; set; }

    /// <summary>Время восхода солнца.</summary>
    public string Sunrise { get; set; } = string.Empty;

    /// <summary>Время заката солнца.</summary>
    public string Sunset { get; set; } = string.Empty;

    /// <summary>Почасовой прогноз (24 записи).</summary>
    public ICollection<Hour>? Hours { get; set; }
}