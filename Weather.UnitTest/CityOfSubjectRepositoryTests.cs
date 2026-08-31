using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Weather.Infrastructure.DataLayer;
using Weather.Infrastructure.Implementations;

namespace Weather.UnitTest;

public class CityOfSubjectRepositoryTests
{
    private readonly CityOfSubjectRepository _repository;

    public CityOfSubjectRepositoryTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        var dataContext = new DataContext(options);
        _repository = new CityOfSubjectRepository(dataContext);
    }

    [Fact]
    public async Task GetAll_ShouldReturnEntries()
    {
        var result = await _repository.GetAll();
        Assert.True(result.Count > 0);
    }

    [Fact]
    public async Task GetAll_EveryEntry_ShouldHaveNonEmptyId()
    {
        var result = await _repository.GetAll();
        Assert.All(result, entry => Assert.NotEqual(Guid.Empty, entry.Id));
    }

    [Fact]
    public async Task GetAll_EveryEntry_ShouldHaveRequiredFields()
    {
        var result = await _repository.GetAll();
        Assert.All(result, entry =>
        {
            Assert.False(string.IsNullOrWhiteSpace(entry.Subject));
            Assert.False(string.IsNullOrWhiteSpace(entry.City));
            Assert.NotEqual(0, entry.Lat);
            Assert.NotEqual(0, entry.Lon);
        });
    }
}