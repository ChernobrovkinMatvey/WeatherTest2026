using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Weather.Application.Services;
using Weather.Infrastructure.Implementation;

namespace Weather.Infrastructure;

public static class DependencyInjection
{
    extension(IServiceCollection services)
    {
        /// <summary>
        /// Регистрирует в DI все профили AutoMapper из сборки Weather.Infrastructure.
        /// </summary>
        public IServiceCollection AddMappingProfiles()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(DependencyInjection).Assembly);
            }, null);

            services.AddSingleton<IMapper>(config.CreateMapper());

            return services;
        }

        /// <summary>
        /// Регистрирует сервисы приложения.
        /// </summary>
        public IServiceCollection AddServices()
        {
            services.AddScoped<IWeatherService, WeatherService>();

            return services;
        }

        /// <summary>
        /// Регистрирует все FluentValidation-валидаторы из сборки Weather.Infrastructure.
        /// </summary>
        public IServiceCollection AddValidators()
        {
            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            return services;
        }

        /// <summary>
        /// Регистрирует все MediatR-обработчики из сборки Weather.Infrastructure.
        /// </summary>
        public IServiceCollection AddHandlers()
        {
            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            return services;
        }
    }
}