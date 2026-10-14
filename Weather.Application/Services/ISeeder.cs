namespace Weather.Application.Services;

/// <summary>
/// Загрузчик справочника в БД
/// </summary>
public interface ISeeder
{
    /// <summary>
    /// Загружает данные
    /// </summary>
    void Seed();
}