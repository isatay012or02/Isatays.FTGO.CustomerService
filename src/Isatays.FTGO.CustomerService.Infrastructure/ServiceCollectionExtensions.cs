using Isatays.FTGO.CustomerService.Core.Interfaces;
using Isatays.FTGO.CustomerService.Infrastructure.Persistence;
using Isatays.FTGO.CustomerService.Infrastructure.Services;
using MassTransit;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Isatays.FTGO.CustomerService.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection ConfigureInfrastructurePersistence(this IServiceCollection services, IConfiguration configuration, string environmentName)
    {
        var connectionString = configuration.GetConnectionString("Ftgo")!;

        services.AddDbContext<DataContext>(options =>
        {
            options.UseNpgsql(connectionString,
                sqlOptions =>
                {
                    sqlOptions.EnableRetryOnFailure(
                        3,
                        TimeSpan.FromSeconds(10),
                        null);
                });
        });

        return services;
    }

    public static IServiceCollection ConfigureInfrastructureServices(this IServiceCollection services)
    {
        //services.AddScoped<ICustomerService, Services.CustomerService>();
        //services.AddScoped<IRabbitMqService, RabbitMqService>();
        services.AddScoped<IDataContext, DataContext>();

        return services;
    }

    public static IServiceCollection ConfigureInfrastructureMassTransit(this IServiceCollection services)
    {
       

        services.AddMassTransit(x =>
        {
            x.AddConsumer<CheckCustomerConsumer>();
            x.UsingRabbitMq();
        });

        services.AddMassTransitHostedService();

        return services;
    }
}
