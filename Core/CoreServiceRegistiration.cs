using Core.Pipelines.Authorization;
using Core.Utilities.JWT;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Core;

public static class CoreServiceRegistiration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, TokenOptions tokenOptions)
    {
        services.AddScoped<ITokenHelper, JwtHelper>(_ => new JwtHelper(tokenOptions));
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
        });

        return services;
    }
}
