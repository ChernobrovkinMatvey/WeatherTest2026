namespace Weather.Application.Contracts;

/// <summary>
/// Контракт текущих погодных условий для внешнего API
/// </summary>
public class CurrentWeatherContract
{
    /// <summary>Температура воздуха в градусах Цельсия.</summary>
    public double TempC { get; set; }

    /// <summary>Ощущаемая температура в градусах Цельсия.</summary>
    public double FeelsLikeC { get; set; }

    /// <summary>Относительная влажность воздуха в процентах.</summary>
    public int Humidity { get; set; }

    /// <summary>Скорость ветра в км/ч.</summary>
    public double WindKph { get; set; }

    /// <summary>Скорость ветра в м/с.</summary>
    public double WindMps { get; set; }

    /// <summary>УФ-индекс.</summary>
    public double Uv { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherConditionContract? Condition { get; set; }

    /// <summary>Дата и время последнего обновления данных.</summary>
    public string LastUpdated { get; set; } = string.Empty;
}