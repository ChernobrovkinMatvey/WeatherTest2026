using MediatR;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на получение списка административных центров субъектов Российской Федерации
/// </summary>
public class GetCityOfSubjectQuery : IRequest<ICollection<CityOfSubject>>
{
    //
}