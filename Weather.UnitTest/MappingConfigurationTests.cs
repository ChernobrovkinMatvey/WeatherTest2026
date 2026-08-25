using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.UnitTest;

public class MappingConfigurationTests
{
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