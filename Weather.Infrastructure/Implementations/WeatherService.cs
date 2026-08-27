using System.Globalization;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Weather.Application.Services;
using Weather.Application.WeatherApi;
using Weather.Infrastructure.Configurations;

namespace Weather.Infrastructure.Implementations;

/// <summary>
/// Реализация сервиса погоды через WeatherAPI.com
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
        using var cts = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
        cts.CancelAfter(TimeSpan.FromMilliseconds(_options.TimeoutMs));

        var client = _httpClientFactory.CreateClient();
        var pathParam = days.HasValue ? "forecast" : "current";
        var daysParam = days.HasValue ? $"&days={days.Value}" : "";
        var url = $"{_options.BaseUrl}/{pathParam}.json?key={_options.ApiKey}&q={lat.ToString(provider: CultureInfo.InvariantCulture)},{lon.ToString(provider: CultureInfo.InvariantCulture)}{daysParam}";

        var httpResponse = await client.GetAsync(url, cts.Token);
        httpResponse.EnsureSuccessStatusCode();

        var content = await httpResponse.Content.ReadAsStringAsync(cts.Token);

        return JsonSerializer.Deserialize<WeatherRawResponse>(content)
               ?? throw new InvalidOperationException("Failed to deserialize weather API response.");
    }
}