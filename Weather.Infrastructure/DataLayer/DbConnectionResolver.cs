using Microsoft.Extensions.Configuration;

namespace Weather.Infrastructure.DataLayer;

/// <summary>
///     Резолвер строки подключения к БД.
///     Сначала проверяет переменную окружения DATABASE_URL.
///     Если не задана — читает "ConnectionStrings:DefaultConnection" из переданного <see cref="IConfiguration"/>.
///     Если configuration не передан (design-time), строит свою конфигурацию из appsettings.json проекта.
/// </summary>
internal static class DbConnectionResolver
{
    /// <summary>
    ///     Возвращает строку подключения к БД.
    /// </summary>
    /// <param name="configuration">
    ///     Конфигурация приложения (передаётся из DI).
    ///     Если null — будет прочитан appsettings.json из директории проекта (для design-time фабрики).
    /// </param>
    /// <returns>
    ///     Строка подключения, или null если не удалось определить.
    /// </returns>
    public static string? ResolveConnectionString(IConfiguration? configuration = null)
    {
        var connectionString = Environment.GetEnvironmentVariable("DATABASE_URL");
        if (!string.IsNullOrWhiteSpace(connectionString))
            return connectionString;

        // для создания миграций dotnet ef migrations add InitialCreate --project Weather.Infrastructure
        configuration ??= new ConfigurationBuilder()
            .SetBasePath(Environment.GetEnvironmentVariable("MSBuildProjectDirectory")
                         ?? Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: true)
            .Build();

        return configuration.GetConnectionString("DefaultConnection");
    }
}