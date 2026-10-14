using Microsoft.EntityFrameworkCore;
using Weather.Application.Services;
using Weather.Infrastructure;
using Weather.Infrastructure.Configurations;
using Weather.Infrastructure.DataLayer;
using WeatherUi.Components;

namespace WeatherUi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddRazorComponents()
            .AddInteractiveServerComponents();

        builder.Services
            .Configure<WeatherApiOptions>(builder.Configuration.GetSection(WeatherApiOptions.SectionName));

        builder.Services.AddMappingProfiles();
        builder.Services.AddInfrastructureServices(builder.Configuration);
        builder.Services.AddValidators();
        builder.Services.AddHandlers();

        var app = builder.Build();
        
        // запуск миграций и сидеров
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<DataContext>();
            if (context.Database.IsRelational())
                context.Database.Migrate();
            var seeders = scope.ServiceProvider.GetRequiredService<IEnumerable<ISeeder>>();
            foreach (var seeder in seeders)
                seeder.Seed();  
        }

        if (!app.Environment.IsDevelopment())
        {
            app.UseHsts();
        }

        app.UseStatusCodePagesWithReExecute("/not-found", createScopeForStatusCodePages: true);
        app.UseHttpsRedirection();
        app.UseAntiforgery();
        app.MapStaticAssets();
        app.MapRazorComponents<App>()
            .AddInteractiveServerRenderMode();

        app.Run();
    }
}
