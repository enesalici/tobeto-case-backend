using Core.Utilities.JWT;
using Microsoft.Extensions.DependencyInjection;

namespace Core;

public static class CoreServiceRegistiration
{
    public static IServiceCollection AddCoreServices(this IServiceCollection services, TokenOptions tokenOptions)
    {
        services.AddScoped<ITokenHelper, JwtHelper>(_ => new JwtHelper(tokenOptions));
        
        return services;
    }
}
