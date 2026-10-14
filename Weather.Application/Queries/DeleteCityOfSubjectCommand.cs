using MediatR;
using Weather.Application.DTOs;
using Weather.Domain.Entities;

namespace Weather.Application.Queries;

/// <summary>
/// Запрос на удаление административного центра субъекта Российской Федерации
/// </summary>
public class DeleteCityOfSubjectCommand : IRequest<int>
{
    public Guid Id { get; set; }
}