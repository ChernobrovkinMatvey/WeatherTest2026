using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Возвращает статический список административных центров с координатами <see cref="GetCityOfSubjectQuery"/>
/// </summary>
public class GetCityOfSubjectHandler(IReadRepository<CityOfSubject> cityOfSubjectRepository) : IRequestHandler<GetCityOfSubjectQuery, ICollection<CityOfSubject>>
{
    private readonly IReadRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<ICollection<CityOfSubject>> Handle(GetCityOfSubjectQuery request, CancellationToken cancellationToken)
    {
        return await _cityOfSubjectRepository.GetAll();
    }
}