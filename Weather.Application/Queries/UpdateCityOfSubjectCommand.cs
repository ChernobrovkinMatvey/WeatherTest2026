using MediatR;
using Weather.Application.DTOs;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на изменения административного центра субъекта Российской Федерации
/// </summary>
public class UpdateCityOfSubjectCommand : IRequest<CityOfSubject>
{
    public Guid Id { get; set; }
        
    public CityOfSubjectPushDto UpdateDto { get; set; }
}