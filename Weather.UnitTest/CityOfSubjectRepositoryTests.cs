using Weather.Infrastructure.Implementations;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для CityOfSubjectRepository
/// </summary>
public class CityOfSubjectRepositoryTests
{
    /// <summary>
    /// Проверяет, что GetAll возвращает хотя бы одну запись
    /// </summary>
    [Fact]
    public async Task GetAll_ShouldReturnEntries()
    {
        var repository = new CityOfSubjectRepository();
        var result = await repository.GetAll();
        Assert.True(result.Count > 0);
    }

    /// <summary>
    /// Проверяет, что у каждой записи заполнен Id
    /// </summary>
    [Fact]
    public async Task GetAll_EveryEntry_ShouldHaveNonEmptyId()
    {
        var repository = new CityOfSubjectRepository();
        var result = await repository.GetAll();
        Assert.All(result, entry => Assert.NotEqual(Guid.Empty, entry.Id));
    }

    /// <summary>
    /// Проверяет, что у каждой записи заполнены Subject, City и координаты
    /// </summary>
    [Fact]
    public async Task GetAll_EveryEntry_ShouldHaveRequiredFields()
    {
        var repository = new CityOfSubjectRepository();
        var result = await repository.GetAll();
        Assert.All(result, entry =>
        {
            Assert.False(string.IsNullOrWhiteSpace(entry.Subject));
            Assert.False(string.IsNullOrWhiteSpace(entry.City));
            Assert.NotEqual(0, entry.Lat);
            Assert.NotEqual(0, entry.Lon);
        });
    }
}