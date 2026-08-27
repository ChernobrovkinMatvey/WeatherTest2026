using FluentValidation.TestHelper;
using Weather.Application.Queries;
using Weather.Infrastructure.Validators;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для валидатора GetWeatherQueryValidator
/// </summary>
public class GetWeatherQueryValidatorTests
{
    private readonly GetWeatherQueryValidator _validator = new();

    /// <summary>
    /// Проверяет, что Days=null проходит валидацию
    /// </summary>
    [Fact]
    public void Validate_WhenDaysIsNull_ShouldPass()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = null };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Проверяет, что Days=3 проходит валидацию
    /// </summary>
    [Fact]
    public void Validate_WhenDaysIs3_ShouldPass()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = 3 };
        var result = _validator.TestValidate(query);
        result.ShouldNotHaveAnyValidationErrors();
    }

    /// <summary>
    /// Проверяет, что Days=0 не проходит валидацию
    /// </summary>
    [Fact]
    public void Validate_WhenDaysIs0_ShouldFail()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = 0 };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Days);
    }

    /// <summary>
    /// Проверяет, что Days=1 не проходит валидацию
    /// </summary>
    [Fact]
    public void Validate_WhenDaysIs1_ShouldFail()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = 1 };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Days);
    }

    /// <summary>
    /// Проверяет, что Days=5 не проходит валидацию
    /// </summary>
    [Fact]
    public void Validate_WhenDaysIs5_ShouldFail()
    {
        var query = new GetWeatherQuery { Lat = 55.75, Lon = 37.61, Days = 5 };
        var result = _validator.TestValidate(query);
        result.ShouldHaveValidationErrorFor(x => x.Days);
    }
}