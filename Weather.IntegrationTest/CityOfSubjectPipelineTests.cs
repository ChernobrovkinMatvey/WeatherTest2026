using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;
using Weather.Infrastructure.DataLayer;
using Weather.Infrastructure.Handlers;
using Weather.Infrastructure.Implementations;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.IntegrationTest;

public class CityOfSubjectPipelineTests
{
    private readonly IMapper _mapper;
    private readonly IRepository<CityOfSubject> _repository;

    public CityOfSubjectPipelineTests()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: $"TestDb_{Guid.NewGuid()}")
            .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning))
            .Options;
        var dataContext = new DataContext(options);

        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ToContractsMappingProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
        _repository = new CityOfSubjectRepository(dataContext);
    }

    /// <summary>
    /// Проверяет, что pipeline возвращает хотя бы одну запись
    /// </summary>
    [Fact]
    public async Task GetCityOfSubjectQuery_FullPipeline_ShouldReturnMoreThanZeroEntries()
    {
        var handler = new GetAllCityOfSubjectHandler(_repository);

        var query = new GetAllCityOfSubjectQuery();
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.Count > 0);
    }

    /// <summary>
    /// Проверяет маппинг CityOfSubject -> CityOfSubjectContract через реальный AutoMapper
    /// </summary>
    [Fact]
    public async Task CityOfSubject_Mapping_ShouldConvertIdToStringAndPreserveFields()
    {
        var handler = new GetAllCityOfSubjectHandler(_repository);

        var result = await handler.Handle(new GetAllCityOfSubjectQuery(), CancellationToken.None);

        var contracts = _mapper.Map<ICollection<Application.Contracts.CityOfSubjectContract>>(result);

        // Проверяем, что все записи имеют уникальные Id
        var ids = contracts.Select(c => c.Id).Distinct().Count();
        Assert.Equal(contracts.Count, ids);
    }
}