using MediatR;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на получение административного центра субъекта Российской Федерации
/// </summary>
public class GetCityOfSubjectQuery : IRequest<CityOfSubject?>
{
    public Guid Id { get; set; }
}