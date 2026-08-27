using Moq;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;
using Weather.Infrastructure.Handlers;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для GetCityOfSubjectHandler с использованием Moq
/// </summary>
public class GetCityOfSubjectHandlerTests
{
    private readonly Mock<IReadRepository<CityOfSubject>> _repositoryMock;

    private readonly GetCityOfSubjectHandler _handler;

    public GetCityOfSubjectHandlerTests()
    {
        _repositoryMock = new Mock<IReadRepository<CityOfSubject>>();
        _handler = new GetCityOfSubjectHandler(_repositoryMock.Object);
    }

    /// <summary>
    /// Проверяет, что Handle делегирует вызов репозиторию GetAll
    /// </summary>
    [Fact]
    public async Task Handle_ShouldDelegateToRepository()
    {
        var expectedCities = new List<CityOfSubject>
        {
            new CityOfSubject { Id = Guid.NewGuid(), Subject = "Москва", City = "Москва", Lat = 55.75, Lon = 37.61 }
        };

        _repositoryMock
            .Setup(r => r.GetAll())
            .ReturnsAsync(expectedCities);

        var result = await _handler.Handle(new GetCityOfSubjectQuery(), CancellationToken.None);

        Assert.Same(expectedCities, result);
        _repositoryMock.Verify(r => r.GetAll(), Times.Once);
    }
}