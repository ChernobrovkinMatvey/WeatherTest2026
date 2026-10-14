using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
using Scrutor;
using Weather.Application.Services;
using Weather.Domain.Entities;
using Weather.Infrastructure.DataLayer;
using Weather.Infrastructure.Implementations;

namespace Weather.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Регистрирует в DI все профили AutoMapper из сборки Weather.Infrastructure
        /// </summary>
        public IServiceCollection AddMappingProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DependencyInjection).Assembly);
            }, NullLoggerFactory.Instance);

            services.AddSingleton<IMapper>(config.CreateMapper());

            return services;
        }

        /// <summary>
        /// Регистрация загрузчиков справочников
        /// </summary>
        private void RegisterSeeders()
        { 
            // var seederTypes = typeof(DependencyInjection).Assembly.GetTypes()
            //     .Where(t => t is { IsClass: true, IsAbstract: false } &&
            //                 typeof(ISeeder).IsAssignableFrom(t) &&
            //                 t.Namespace == "Weather.Infrastructure.Seeders");
            // foreach (var seederType in seederTypes)
            //     services.AddScoped(typeof(ISeeder), seederType);

            services.Scan(scan => scan
                .FromAssemblies(typeof(DependencyInjection).Assembly)
                .AddClasses(classes => classes.AssignableTo<ISeeder>())
                .AsImplementedInterfaces()
                .WithScopedLifetime()); 
        }

        /// <summary>
        /// Регистрирует сервисы приложения
        /// </summary>
        public IServiceCollection AddInfrastructureServices(IConfiguration configuration)
        {
            services.AddHttpClient();

            services.AddDbContext<DataContext>(options =>
            {
                var connectionString = DbConnectionResolver.ResolveConnectionString(configuration);

                if (!string.IsNullOrWhiteSpace(connectionString))
                {
                    options.UseNpgsql(connectionString);
                    return;
                }

                options.UseInMemoryDatabase("DataContext")
                    .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });

            services.RegisterSeeders();
            
            services.AddScoped<IWeatherService, WeatherService>();
            services.AddScoped<IRepository<CityOfSubject>, CityOfSubjectRepository>();

            return services;
        }

        /// <summary>
        /// Регистрирует все FluentValidation-валидаторы из сборки Weather.Infrastructure
        /// </summary>
        public IServiceCollection AddValidators()
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }

        /// <summary>
        /// Регистрирует все MediatR-обработчики из сборки Weather.Infrastructure
        /// </summary>
        public IServiceCollection AddHandlers()
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}