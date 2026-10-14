using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Weather.Infrastructure.DataLayer;

/// <summary>
///     Design-time фабрика для EF Core CLI (dotnet ef migrations add / remove и т.д.).
///     Позволяет создавать <see cref="DataContext"/> без запуска Web-приложения,
///     используя <see cref="DbConnectionResolver"/> для получения строки подключения.
/// </summary>
public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    /// <summary>
    ///     Создаёт экземпляр <see cref="DataContext"/> для дизайн-тайма.
    /// </summary>
    /// <param name="args">Аргументы, переданные после -- в dotnet ef (не используются).</param>
    /// <returns>Сконфигурированный DataContext.</returns>
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var connectionString = DbConnectionResolver.ResolveConnectionString();

        if (!string.IsNullOrWhiteSpace(connectionString))
            optionsBuilder.UseNpgsql(connectionString);
        else
            optionsBuilder.UseInMemoryDatabase("DataContext");

        return new DataContext(optionsBuilder.Options);
    }
}