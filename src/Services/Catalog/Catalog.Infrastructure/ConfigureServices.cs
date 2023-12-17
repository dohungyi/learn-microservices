using Catalog.Application.Persistence;
using Catalog.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Catalog.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContextPool<ApplicationDbContext>((provider, options) =>
        {
            options.UseMySql(
                    connectionString: CoreSettings.ConnectionStrings["CatalogDb"],
                    serverVersion: ServerVersion.AutoDetect(CoreSettings.ConnectionStrings["CatalogDb"]))
                .LogTo(s => System.Diagnostics.Debug.WriteLine(s))
                .EnableDetailedErrors(true)
                .EnableSensitiveDataLogging(true);
        });
        
        
        services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<IAppDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
        
        services.AddScoped<ApplicationDbContextSeed>();
        
        // Base
        services.AddScoped(typeof(IEfCoreReadOnlyRepository<,>), typeof(EfCoreReadOnlyRepository<,>));
        services.AddScoped(typeof(IEfCoreWriteOnlyRepository<,>), typeof(EfCoreWriteOnlyRepository<,>));

        return services;
    }
}