

using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application;

public static class ApplicationServiceRegistiration
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly());
            //config.AddOpenBehavior(typeof(LoggingBehavior<,>));
            //config.AddOpenBehavior(typeof(AuthorizationBehavior<,>));
            //config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        //services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        return services;
    }
}
