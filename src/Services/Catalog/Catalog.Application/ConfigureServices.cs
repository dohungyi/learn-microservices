using System.Reflection;
using Catalog.Application.Behaviors;
using Catalog.Application.Services.Implements;
using Catalog.Application.Services.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.Configure;
using SharedKernel.Infrastructures;

namespace Catalog.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Auto Mapper
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        
        // Fluent Validator
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        
        // MediaR
        services.AddMediatR(cfg => {
            cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });

        // Pipelines
        services.AddCoreBehaviors();
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

        services.AddTransient<ICachingService, CachingService>();
        
        return services;
    }
}