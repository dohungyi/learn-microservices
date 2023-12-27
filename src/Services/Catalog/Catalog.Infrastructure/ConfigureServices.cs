using Catalog.Application.Persistence;
using Catalog.Application.Repositories;
using Catalog.Infrastructure.Persistence;
using Catalog.Infrastructure.Repositories;
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
                    connectionString: CoreSettings.ConnectionStrings["MasterDb"],
                    serverVersion: ServerVersion.AutoDetect(CoreSettings.ConnectionStrings["MasterDb"]))
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
        
        // Supplier
        services.AddScoped<ISupplierWriteOnlyRepository, SupplierWriteOnlyRepository>();
        services.AddScoped<ISupplierReadOnlyRepository, SupplierReadOnlyRepository>();
        
        // Category
        services.AddScoped<ICategoryWriteOnlyRepository, CategoryWriteOnlyRepository>();
        services.AddScoped<ICategoryReadOnlyRepository, ICategoryReadOnlyRepository>();

        return services;
    }
}