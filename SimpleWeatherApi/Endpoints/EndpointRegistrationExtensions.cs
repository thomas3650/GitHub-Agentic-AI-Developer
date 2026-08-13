using System.Reflection;

namespace SimpleWeatherApi.Endpoints;

public static class EndpointRegistrationExtensions
{
    public static IServiceCollection AddEndpoints(this IServiceCollection services, Assembly? assembly = null)
    {
        assembly ??= Assembly.GetCallingAssembly();

        var endpointType = typeof(IEndpoint);
        var endpointTypes = assembly.GetTypes()
            .Where(t => t is { IsClass: true, IsAbstract: false } && endpointType.IsAssignableFrom(t));

        foreach (var type in endpointTypes)
        {
            services.AddTransient(endpointType, type);
        }

        return services;
    }

    public static WebApplication MapEndpoints(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var endpoints = scope.ServiceProvider.GetServices<IEndpoint>();

        foreach (var endpoint in endpoints)
        {
            endpoint.MapEndpoint(app);
        }

        return app;
    }
}
