using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Возвращает административный центр с координатами <see cref="GetCityOfSubjectQuery"/>
/// </summary>
public class GetCityOfSubjectHandler(IRepository<CityOfSubject> cityOfSubjectRepository) : IRequestHandler<GetCityOfSubjectQuery, CityOfSubject?>
{
    private readonly IRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<CityOfSubject?> Handle(GetCityOfSubjectQuery request, CancellationToken cancellationToken)
    { 
        var cityOfSubject = await _cityOfSubjectRepository.GetById(request.Id);

        return cityOfSubject;
    }
}