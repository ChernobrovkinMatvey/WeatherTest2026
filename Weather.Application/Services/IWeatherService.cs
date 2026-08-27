using Weather.Application.WeatherApi;

namespace Weather.Application.Services;

/// <summary>
/// Сервис для получения сырых данных от внешнего API погоды
/// </summary>
public interface IWeatherService
{
    /// <summary>
    /// Получает сырой ответ от WeatherAPI (forecast.json?days=3) для указанных координат
    /// </summary>
    Task<WeatherRawResponse> GetWeather(double lat, double lon, int? days, CancellationToken cancellationToken = default);
}