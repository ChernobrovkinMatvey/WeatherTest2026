using System.Reflection;
using Microsoft.OpenApi;
using Weather.Infrastructure;

namespace Weather.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
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
        builder.Services.AddInfrastructureServices();
        builder.Services.AddValidators();
        builder.Services.AddHandlers();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}