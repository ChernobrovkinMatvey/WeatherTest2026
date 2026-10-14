using Weather.Application.Services;
using Weather.Domain;
using Weather.Domain.Entities;
using Weather.Infrastructure.DataLayer;

namespace Weather.Infrastructure.Implementations;

/// <summary>
/// Репозиторий с городами субъектов Российской Федерации
/// </summary>
public class CityOfSubjectRepository(DataContext dataContext) : IRepository<CityOfSubject>
{
    /// <inheritdoc/>
    public async Task<ICollection<CityOfSubject>> GetAll()
    {
        return dataContext.Set<CityOfSubject>().ToList();
    }

    /// <inheritdoc/>
    public async Task<CityOfSubject?> GetById(Guid id)
    {
        return await dataContext.Set<CityOfSubject>().FindAsync(id);
    }

    /// <inheritdoc/>
    public async Task<int> Add(CityOfSubject entity)
    {
        if (entity.Id == Guid.Empty)
            entity.Id = Guid.NewGuid();
        
        await dataContext.Set<CityOfSubject>().AddAsync(entity);
        return await dataContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<int> Update(CityOfSubject entity)
    {
        var cityOfSubject = await GetById(entity.Id) ?? throw new EntityNotFoundException($"Сущность не найдена по id ({entity.Id})");
        cityOfSubject.City = entity.City;
        cityOfSubject.Subject = entity.Subject;
        cityOfSubject.Lat = entity.Lat;
        cityOfSubject.Lon = entity.Lon;
        
        return await dataContext.SaveChangesAsync();
    }

    /// <inheritdoc/>
    public async Task<int> Delete(Guid id)
    {
        var entity = await GetById(id) ?? throw new EntityNotFoundException($"Сущность не найдена по id ({id})");
        
        dataContext.Set<CityOfSubject>().Remove(entity);
        return await dataContext.SaveChangesAsync();
    }
}