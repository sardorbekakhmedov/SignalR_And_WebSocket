using System.Reflection;

namespace WebSocket.Server_1.WebSocketManager;

public static class SocketsExtension
{
    public static IServiceCollection AddWebSocketManager(this IServiceCollection services)
    {
        services.AddTransient<ConnectionManager>();

        var exportedTypes = Assembly.GetEntryAssembly()?.ExportedTypes;

        if (exportedTypes is null)
            return services;

        foreach (var type in exportedTypes)
        {
            if (type.GetTypeInfo().BaseType == typeof(SocketHandler))
                services.AddSingleton(type);
        }

        return services;
    }

    public static IApplicationBuilder MapSockets(this IApplicationBuilder app, string path, SocketHandler socketHandler)
    {
        return app.Map(path, (x) => x.UseMiddleware<SocketMiddleware>(socketHandler));
    }
}