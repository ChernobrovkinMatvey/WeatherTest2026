using Weather.Infrastructure;
using Weather.Infrastructure.Configurations;
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

        builder.Services.AddHttpClient();

        builder.Services.AddMappingProfiles();
        builder.Services.AddServices();
        builder.Services.AddValidators();
        builder.Services.AddHandlers();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Error", createScopeForErrors: true);
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
