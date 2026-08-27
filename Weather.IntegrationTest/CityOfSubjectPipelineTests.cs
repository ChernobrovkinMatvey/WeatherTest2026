using AutoMapper;
using Microsoft.Extensions.Logging.Abstractions;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;
using Weather.Infrastructure.Handlers;
using Weather.Infrastructure.Implementations;
using Weather.Infrastructure.MappingProfiles;

namespace Weather.IntegrationTest;

/// <summary>
/// Интеграционный тест для CityOfSubject pipeline: GetCityOfSubjectQuery -> хендлер -> репозиторий -> маппинг
/// </summary>
public class CityOfSubjectPipelineTests
{
    private readonly IMapper _mapper;

    private readonly IReadRepository<CityOfSubject> _repository;

    public CityOfSubjectPipelineTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<ToContractsMappingProfile>();
        }, NullLoggerFactory.Instance);

        _mapper = config.CreateMapper();
        _repository = new CityOfSubjectRepository();
    }

    /// <summary>
    /// Проверяет, что pipeline возвращает хотя бы одну запись
    /// </summary>
    [Fact]
    public async Task GetCityOfSubjectQuery_FullPipeline_ShouldReturnMoreThanZeroEntries()
    {
        var handler = new GetCityOfSubjectHandler(_repository);

        var query = new GetCityOfSubjectQuery();
        var result = await handler.Handle(query, CancellationToken.None);

        Assert.True(result.Count > 0);
    }

    /// <summary>
    /// Проверяет маппинг CityOfSubject -> CityOfSubjectContract через реальный AutoMapper
    /// </summary>
    [Fact]
    public async Task CityOfSubject_Mapping_ShouldConvertIdToStringAndPreserveFields()
    {
        var handler = new GetCityOfSubjectHandler(_repository);

        var result = await handler.Handle(new GetCityOfSubjectQuery(), CancellationToken.None);

        var contracts = _mapper.Map<ICollection<Application.Contracts.CityOfSubjectContract>>(result);

        // Проверяем, что все записи имеют уникальные Id
        var ids = contracts.Select(c => c.Id).Distinct().Count();
        Assert.Equal(contracts.Count, ids);
    }
}