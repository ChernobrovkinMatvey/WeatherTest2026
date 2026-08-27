namespace Weather.Domain.Models;

/// <summary>
/// Текущие погодные условия
/// </summary>
public class CurrentWeather
{
    /// <summary>Температура воздуха в градусах Цельсия.</summary>
    public double TempC { get; set; }

    /// <summary>Ощущаемая температура в градусах Цельсия.</summary>
    public double FeelsLikeC { get; set; }

    /// <summary>Относительная влажность воздуха в процентах.</summary>
    public int Humidity { get; set; }

    /// <summary>Скорость ветра в км/ч.</summary>
    public double WindKph { get; set; }

    /// <summary>УФ-индекс.</summary>
    public double Uv { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherCondition? Condition { get; set; }

    /// <summary>Дата и время последнего обновления данных.</summary>
    public string LastUpdated { get; set; } = string.Empty;
}