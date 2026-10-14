using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using Weather.Application.Services;
using Weather.Infrastructure;
using Weather.Infrastructure.DataLayer;

namespace Weather.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddProblemDetails();
        builder.Services.AddExceptionHandler<Exceptions.ExceptionHandler>();
        builder.Services.AddOpenApi();
        builder.Services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Demo API", Version = "v1" });
            // Указываем Swashbuckle комментарии из xml файлов
            var executeDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
            foreach (var xmlFile in Directory.GetFiles(executeDirectory, "*.Xml", SearchOption.TopDirectoryOnly))
            {
                c.IncludeXmlComments(xmlFile);
            }
        });
        
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

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseExceptionHandler();

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}