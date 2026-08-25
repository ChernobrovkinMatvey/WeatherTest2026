namespace Weather.Domain.Models;

/// <summary>
/// Почасовой прогноз погоды.
/// </summary>
public class Hour
{
    /// <summary>Дата и время часа прогноза.</summary>
    public string Time { get; set; }

    /// <summary>Температура воздуха в градусах Цельсия.</summary>
    public double TempC { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherCondition? Condition { get; set; }

    /// <summary>Вероятность дождя в процентах.</summary>
    public int ChanceOfRain { get; set; }
}