using AutoMapper;
using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Обновляет административный центр
/// </summary>
public class UpdateCityOfSubjectHandler(IRepository<CityOfSubject> cityOfSubjectRepository, IMapper mapper)
    : IRequestHandler<UpdateCityOfSubjectCommand, CityOfSubject>
{
    private readonly IMapper _mapper = mapper;
    
    private readonly IRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<CityOfSubject> Handle(UpdateCityOfSubjectCommand request, CancellationToken cancellationToken)
    {
        var cityOfSubject = _mapper.Map<CityOfSubject>(request.UpdateDto);
        cityOfSubject.Id = request.Id;
        
        await _cityOfSubjectRepository.Update(cityOfSubject);

        //return await _cityOfSubjectRepository.GetById(request.Id);
        return cityOfSubject;
    }
}