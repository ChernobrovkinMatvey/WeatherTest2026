namespace Weather.Application.Contracts;

/// <summary>
/// Контракт почасового прогноза погоды для внешнего API
/// </summary>
public class HourContract
{
    /// <summary>Дата и время часа прогноза.</summary>
    public string Time { get; set; } = string.Empty;

    /// <summary>Температура воздуха в градусах Цельсия.</summary>
    public double TempC { get; set; }

    /// <summary>Погодное условие (текст, иконка).</summary>
    public WeatherConditionContract? Condition { get; set; }

    /// <summary>Вероятность дождя в процентах.</summary>
    public int ChanceOfRain { get; set; }
}