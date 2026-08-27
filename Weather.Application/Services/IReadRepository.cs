using Weather.Domain;

namespace Weather.Application.Services;

/// <summary>
/// Репозиторий сущностей для чтения
/// </summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IReadRepository<T> where T: Entity
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    Task<ICollection<T>> GetAll();
}