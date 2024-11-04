using System.Reflection;
using Serilog;
using Serilog.Events;
using Core.Application;
using Infrastructure.Persistence;
using Core.Auth.Api;
using Core.Api;
using Core.Auth.Application;
using Core.Api.Middlewares;
using Core.Auth.Application.Middlewares;
using Routes.Application;
using Attractions.Application;
using Tours.Application;
using StackExchange.Redis;

try
{
    const string version = "v1";
    const string appName = "TravelAroundBelarus API v1";

    var builder = WebApplication.CreateBuilder(args);

    builder.Host.UseSerilog((ctx, lc) => lc
#if DEBUG
        .WriteTo.Console()
#endif
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Information-.txt", LogEventLevel.Information,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 3, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Warning-.txt", LogEventLevel.Warning,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 14, buffered: true)
        .WriteTo.File($"{builder.Configuration["Logging:LogsFolder"]}/Error-.txt", LogEventLevel.Error,
            rollingInterval: RollingInterval.Day, retainedFileCountLimit: 30, buffered: true));

    // Add CORS policy
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", builder =>
        {
            //    builder.AllowAnyOrigin()
            //           .AllowAnyMethod()
            //           .AllowAnyHeader()
            //           .AllowCredentials();
            //});
            builder.WithOrigins("https://localhost:7125", "https://localhost:7098") // ”кажите конкретные домены
                   .AllowAnyMethod()
                   .AllowAnyHeader()
                   .AllowCredentials(); // ƒл€ работы с куки
        });
    });


    builder.Services
        .AddSwaggerWidthJwtAuth(Assembly.GetExecutingAssembly(), appName, version, appName)
        .AddCoreApiServices()
        .AddCoreApplicationServices()
        .AddCoreAuthApiServices(builder.Configuration)
        .AddPersistenceServices(builder.Configuration)
        .AddCoreAuthServices()
        .AddAllCors()
        .AddToursApplication()
        .AddAttractionsApplication()
        .AddRoutesApplication()
        .AddControllers();


    var app = builder.Build();
    app.UseStaticFiles();
    app.MapControllers();

    app.RunDbMigrations().RegisterApis(Assembly.GetExecutingAssembly(), $"api/{version}");

    app.UseCoreExceptionHandler()
        .UseAuthExceptionHandler()
        .UseSwagger(c => { c.RouteTemplate = "swagger/{documentName}/swagger.json"; })
        .UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            options.RoutePrefix = "swagger";
        })

        .UseHttpsRedirection()
        .UseCors("AllowAll") // Apply CORS policy
        .UseAuthentication()
        .UseAuthorization()
        ;

    app.Run();
}
catch (Exception ex)
{
    var appSettingsFile = $"{Directory.GetCurrentDirectory()}/appsettings.json";
    var settingsJson = File.ReadAllText(appSettingsFile);
    var appSettings = System.Text.Json.JsonDocument.Parse(settingsJson);
    var logsPath = appSettings.RootElement.GetProperty("Logging").GetProperty("LogsFolder").GetString();
    var logger = new LoggerConfiguration()
        .WriteTo.File($"{logsPath}/Log-Run-Error-.txt", LogEventLevel.Error, rollingInterval: RollingInterval.Hour,
            retainedFileCountLimit: 30)
        .CreateLogger();
    logger.Fatal(ex.Message, ex);
}