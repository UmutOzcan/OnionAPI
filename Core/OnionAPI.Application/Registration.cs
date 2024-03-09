using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace OnionAPI.Application;

public static class Registration
{
    // Features daki tüm mediatr islemlerini tanımlayıp DI da kullanırız
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
    }
}
