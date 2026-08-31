using MediatR;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на получение списка административных центров субъектов Российской Федерации
/// </summary>
public class GetAllCityOfSubjectQuery : IRequest<ICollection<CityOfSubject>>
{
    //
}