using Moq;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;
using Weather.Infrastructure.Handlers;

namespace Weather.UnitTest;

/// <summary>
/// Тесты для GetCityOfSubjectHandler с использованием Moq
/// </summary>
public class GetAllCityOfSubjectHandlerTests
{
    private readonly Mock<IRepository<CityOfSubject>> _repositoryMock;

    private readonly GetAllCityOfSubjectHandler _handler;

    public GetAllCityOfSubjectHandlerTests()
    {
        _repositoryMock = new Mock<IRepository<CityOfSubject>>();
        _handler = new GetAllCityOfSubjectHandler(_repositoryMock.Object);
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

        var result = await _handler.Handle(new GetAllCityOfSubjectQuery(), CancellationToken.None);

        Assert.Same(expectedCities, result);
        _repositoryMock.Verify(r => r.GetAll(), Times.Once);
    }
}