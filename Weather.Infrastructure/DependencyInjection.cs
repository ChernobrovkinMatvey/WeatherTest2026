using AutoMapper;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging.Abstractions;
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
        /// Регистрирует сервисы приложения
        /// </summary>
        public IServiceCollection AddInfrastructureServices()
        {
            services.AddHttpClient();

            services.AddDbContext<DataContext>(options =>
            {
                options.UseInMemoryDatabase("DataContext")
                    .ConfigureWarnings(warnings => warnings.Ignore(InMemoryEventId.TransactionIgnoredWarning));
            });
            
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