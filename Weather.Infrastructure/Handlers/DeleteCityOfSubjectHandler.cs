using AutoMapper;
using MediatR;
using Weather.Application.Queries;
using Weather.Application.Services;
using Weather.Domain.Entities;

namespace Weather.Infrastructure.Handlers;

/// <summary>
/// Удаляет административный центр
/// </summary>
public class DeleteCityOfSubjectHandler(IRepository<CityOfSubject> cityOfSubjectRepository)
    : IRequestHandler<DeleteCityOfSubjectCommand, int>
{
    private readonly IRepository<CityOfSubject> _cityOfSubjectRepository = cityOfSubjectRepository;

    public async Task<int> Handle(DeleteCityOfSubjectCommand request, CancellationToken cancellationToken)
    {
        return await _cityOfSubjectRepository.Delete(request.Id);
    }
}