using Weather.Domain;

namespace Weather.Application.Services;

/// <summary>
/// Репозиторий сущностей
/// </summary>
/// <typeparam name="T">Сущность</typeparam>
public interface IRepository<T> where T: Entity
{
    /// <summary>
    /// Получение всех сущностей
    /// </summary>
    Task<ICollection<T>> GetAll();
    
    /// <summary>
    /// Получение сущности по Id
    /// </summary>
    Task<T?> GetById(Guid id);
    
    /// <summary>
    /// Добавление сущности
    /// </summary>
    Task Add(T entity);
    
    /// <summary>
    /// Обновление сущности
    /// </summary>
    Task Update(T entity);
    
    /// <summary>
    /// Удаление сущности
    /// </summary>
    Task Delete(Guid id);
}