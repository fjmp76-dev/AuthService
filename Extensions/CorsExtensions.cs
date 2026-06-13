namespace AuthService.Extensions;

public static class CorsExtensions
{
    public const string AngularPolicy = "AngularPolicy";

    public static IServiceCollection AddAngularCors(this IServiceCollection services, IConfiguration configuration)
    {
        // Puedes mover los orígenes a appsettings también
        var allowedOrigins = configuration
            .GetSection("Cors:AllowedOrigins")
            .Get<string[]>()
            ?? ["http://localhost:4200"];

        services.AddCors(options =>
        {
            options.AddPolicy(AngularPolicy, policy =>
            {
                policy.WithOrigins(allowedOrigins)
                      .AllowAnyHeader()
                      .AllowAnyMethod()
                      .WithExposedHeaders("X-Token-Expired"); // exponer el header custom
            });
        });

        return services;
    }
}