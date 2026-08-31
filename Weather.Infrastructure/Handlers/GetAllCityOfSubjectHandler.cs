using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Возвращает статический список административных центров с координатами <see cref="GetAllCityOfSubjectQuery"/>
/// </summary>
public class GetAllCityOfSubjectHandler(IRepository<CityOfSubject> cityOfSubjectRepository) : IRequestHandler<GetAllCityOfSubjectQuery, ICollection<CityOfSubject>>
{
    private readonly IRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<ICollection<CityOfSubject>> Handle(GetAllCityOfSubjectQuery request, CancellationToken cancellationToken)
    {
        return await _cityOfSubjectRepository.GetAll();
    }
}