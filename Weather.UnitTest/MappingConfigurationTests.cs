using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для проверки корректности конфигурации AutoMapper профилей
/// </summary>
public class MappingConfigurationTests
{
    /// <summary>
    /// Проверяет, что все профили маппинга из сборки Weather.Infrastructure настроены корректно
    /// </summary>
    [Fact]
    public void AllMappingProfiles_ShouldBeValid()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddMaps(typeof(WeatherApiMappingProfile).Assembly);
        }, NullLoggerFactory.Instance);

        config.AssertConfigurationIsValid();
    }
}