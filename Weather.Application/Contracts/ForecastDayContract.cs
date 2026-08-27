namespace Weather.Application.Contracts;

/// <summary>
/// Контракт прогноза погоды на один день для внешнего API
/// </summary>
public class ForecastDayContract
{
    /// <summary>Дата прогноза в формате yyyy-MM-dd.</summary>
    public string Date { get; set; } = string.Empty;

    /// <summary>Средняя температура за день в градусах Цельсия.</summary>
    public double AvgtempC { get; set; }

    /// <summary>Максимальная скорость ветра в км/ч.</summary>
    public double MaxWindKph { get; set; }

    /// <summary>Максимальная скорость ветра в м/с.</summary>
    public double MaxWindMps { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherConditionContract? Condition { get; set; }

    /// <summary>Время восхода солнца.</summary>
    public string Sunrise { get; set; } = string.Empty;

    /// <summary>Время заката солнца.</summary>
    public string Sunset { get; set; } = string.Empty;

    /// <summary>Почасовой прогноз (24 записи).</summary>
    public ICollection<HourContract>? Hours { get; set; }
}