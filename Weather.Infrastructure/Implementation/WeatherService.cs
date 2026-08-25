using Microsoft.Extensions.Options;
using System.Text.Json;
using Weather.Application.WeatherApi;
using Weather.Application.Services;
using Weather.Infrastructure.Configurations;

namespace Weather.Infrastructure.Implementation;

/// <summary>
/// Реализация сервиса погоды через WeatherAPI.com.
/// </summary>
public class WeatherService : IWeatherService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    private readonly WeatherApiOptions _options;

    public WeatherService(IHttpClientFactory httpClientFactory, IOptions<WeatherApiOptions> options)
    {
        _httpClientFactory = httpClientFactory;
        _options = options.Value;
    }

    /// <inheritdoc/>
    public async Task<WeatherRawResponse> GetWeather(double lat, double lon, int? days, CancellationToken cancellationToken = default)
    {
        var client = _httpClientFactory.CreateClient();
        var pathParam = days.HasValue ? "forecast" : "current";
        var daysParam = days.HasValue ? $"&days={days.Value}" : "";
        var url = $"{_options.BaseUrl}/{pathParam}.json?key={_options.ApiKey}&q={lat},{lon}{daysParam}";

        var httpResponse = await client.GetAsync(url, cancellationToken);
        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync(cancellationToken);

        return JsonSerializer.Deserialize<WeatherRawResponse>(content)
               ?? throw new InvalidOperationException("Failed to deserialize weather API response.");
    }
}