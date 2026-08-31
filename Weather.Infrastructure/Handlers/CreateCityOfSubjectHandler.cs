using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Создаёт административный центр с координатами <see cref="CreateCityOfSubjectCommand"/>
/// </summary>
public class CreateCityOfSubjectHandler(IRepository<CityOfSubject> cityOfSubjectRepository)
    : IRequestHandler<CreateCityOfSubjectCommand, CityOfSubject>
{
    private readonly IRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<CityOfSubject> Handle(CreateCityOfSubjectCommand request, CancellationToken cancellationToken)
    {
        var createDto = request.CreateDto;

        var cityOfSubject = new CityOfSubject()
        {
            Id = Guid.Empty,
            City = createDto.City,
            Subject = createDto.Subject,
            Lat = createDto.Lat,
            Lon = createDto.Lon,
        };
        await _cityOfSubjectRepository.Add(cityOfSubject);

        return cityOfSubject;
    }
}