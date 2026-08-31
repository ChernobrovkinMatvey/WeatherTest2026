using MediatR;
using Weather.Application.DTOs;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на добавление административного центра субъекта Российской Федерации
/// </summary>
public class CreateCityOfSubjectCommand : IRequest<CityOfSubject>
{
    public CityOfSubjectCreateDto CreateDto { get; set; }
}