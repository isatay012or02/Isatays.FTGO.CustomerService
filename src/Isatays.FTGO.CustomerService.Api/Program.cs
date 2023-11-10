using Isatays.FTGO.CustomerService.Api.Common.Options;
using Isatays.FTGO.CustomerService.Api.Features.Middleware;
using Isatays.FTGO.CustomerService.Api.Features.Swagger;
using Isatays.FTGO.CustomerService.Api.Features.Versioning;
using Isatays.FTGO.CustomerService.Api.Features.WebApi;
using Isatays.FTGO.CustomerService.Core;
using Isatays.FTGO.CustomerService.Infrastructure;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

var webHostOptions = new WebHostOptions(
    instanceName: builder.Configuration.GetValue<string>($"{WebHostOptions.SectionName}:InstanceName"),
    webAddress: builder.Configuration.GetValue<string>($"{WebHostOptions.SectionName}:WebAddress"));

try
{
    Log.Logger = new LoggerConfiguration().ReadFrom.Configuration(builder.Configuration).CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(Log.Logger);

    builder.ConfigureHostVersioning();

    builder.ConfigureWebHost();

    builder.Services.ConfigureApplicationAssemblies();

    builder.Services
        .ConfigureInfrastructurePersistence(builder.Configuration, builder.Environment.EnvironmentName)
        .ConfigureInfrastructureServices()
        .ConfigureInfrastructureMassTransit();

    var app = builder.Build();

    Log.Information("{Msg} ({ApplicationName})...",
        "Запуск сборки проекта",
        webHostOptions.InstanceName);

    app.UseConfiguredSwagger();
    //app.UseConfiguredVersioning();
    app.UseHttpsRedirection();
    app.UseMiddleware<LoggingMiddleware>();
    app.UseMiddleware<ExceptionHandleMiddleware>();
    app.MapHealthChecks("/healthcheck");

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Программа была выброшена с исплючением ({ApplicationName})!",
        webHostOptions.InstanceName);
}
finally
{
    Log.Information("{Msg}!", "Высвобождение ресурсов логгирования");
    await Log.CloseAndFlushAsync();
}
