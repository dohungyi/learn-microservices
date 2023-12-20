using Caching;
using Catalog.Api.Extensions;
using Catalog.Application.Persistence;
using Catalog.Infrastructure;
using Catalog.Infrastructure.Persistence;
using Microsoft.AspNetCore.HttpOverrides;
using Serilog;
using SharedKernel.Configure;
using SharedKernel.Core;
using SharedKernel.Filters;

var builder = WebApplication.CreateBuilder(args);

var services = builder.Services;
var configuration = builder.Configuration;

try
{
    #region Core settings projects

    // CoreSettings.SetEmailConfig(configuration);
    // CoreSettings.SetS3Config(configuration);
    CoreSettings.SetJwtConfig(configuration);
    CoreSettings.SetConnectionStrings(configuration);

    #endregion
    
    builder.Host.AddAppConfigurations();
    
    #region Add services
    
    services.AddCoreServices(configuration);

    services.AddCoreAuthentication(configuration);
    
    services.AddCoreCaching(configuration);
    
    // services.AddHealthChecks();
    
    services.Configure<ForwardedHeadersOptions>(o => o.ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto);
    
    services.AddCurrentUser();
    
    services.AddEndpointsApiExplorer();
    
    services.AddControllersWithViews(options =>
    {
        options.Filters.Add(new AccessTokenValidatorAsyncFilter());
    });

    services.AddInfrastructureServices(configuration);
    
    #endregion
    
    // Configure the HTTP request pipeline.
    var app = builder.Build();

    #region Pipelines

    if (app.Environment.IsDevelopment())
    {
        app.UseSwaggerVersioning();
    }

    app.UseCoreCors(configuration);

    app.UseCoreConfigure(app.Environment);

    #endregion
    
    #region Initialise and seed database
    
    using (var scope = app.Services.CreateScope())
    {
        var orderContextSeed = scope.ServiceProvider.GetRequiredService<ApplicationDbContextSeed>();
        await orderContextSeed.InitialiseAsync();
        await orderContextSeed.SeedAsync();
    }
    
    #endregion
    
    app.Run();
}
catch (Exception exception)
{
    throw exception;
}
finally
{
    Log.Information("Shut down Catalog Api complete.");
    Log.CloseAndFlush();
}