namespace WeatherUi.Components.Shared;

/// <summary>
/// Вспомогательный класс для форматирования дат из формата WeatherAPI в российский формат
/// </summary>
public static class DateFormatter
{
    /// <summary>
    /// Преобразует дату и время из формата "yyyy-MM-dd HH:mm" в "dd.MM.yyyy HH:mm"
    /// </summary>
    public static string ToLocalDateFormat(string localTime)
    {
        if (string.IsNullOrWhiteSpace(localTime))
            return string.Empty;

        var parts = localTime.Split(' ');
        if (parts.Length != 2)
            return localTime;

        var dateParts = parts[0].Split('-');
        if (dateParts.Length != 3)
            return localTime;

        return $"{dateParts[2]}.{dateParts[1]}.{dateParts[0]} {parts[1]}";
    }

    /// <summary>
    /// Преобразует дату из формата "yyyy-MM-dd" в "dd.MM.yyyy"
    /// </summary>
    public static string ToDateOnly(string date)
    {
        if (string.IsNullOrWhiteSpace(date))
            return string.Empty;

        var parts = date.Split('-');
        if (parts.Length != 3)
            return date;

        return $"{parts[2]}.{parts[1]}.{parts[0]}";
    }

    /// <summary>
    /// Удаляет суффиксы " AM" и " PM" из времени восхода/заката
    /// </summary>
    public static string StripAmPm(string time)
    {
        if (string.IsNullOrWhiteSpace(time))
            return string.Empty;

        return time.Replace(" AM", "").Replace(" PM", "");
    }
}