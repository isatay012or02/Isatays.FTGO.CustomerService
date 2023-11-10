using Asp.Versioning;
using Serilog;

namespace Isatays.FTGO.CustomerService.Api.Features.Versioning;

/// <summary>Предоставляет метод расширения для <see cref="WebApplicationBuilder"/></summary>
public static class VersioningExtensions
{
    /// <summary>Настраивает версионирование для хоста</summary>
    public static WebApplicationBuilder ConfigureHostVersioning(this WebApplicationBuilder builder)
    {
        var apiVersioningBuilder = builder.Services.AddApiVersioning(options =>
        {
            options.AssumeDefaultVersionWhenUnspecified = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.ReportApiVersions = true;
            options.ApiVersionReader = ApiVersionReader.Combine(
                new QueryStringApiVersionReader("api-version"),
                new HeaderApiVersionReader("x-version"),
                new MediaTypeApiVersionReader("ver")
                ); //new UrlSegmentApiVersionReader();
        });

        apiVersioningBuilder.AddApiExplorer(options => {
            //options.ApiVersionParameterSource = new UrlSegmentApiVersionReader()
            options.GroupNameFormat = "'v'VVV";
            options.SubstituteApiVersionInUrl = true;
        });

        Log.Debug("Конфигурация версионирования проекта ({Application})...", builder.Environment.ApplicationName);

        return builder;
    }
}
