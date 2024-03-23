using Microsoft.Extensions.DependencyInjection;
using OnionAPI.Application.Exceptions;
using System.Reflection;

namespace OnionAPI.Application;

public static class Registration
{
    // Features daki tüm cqrs-mediatr islemlerini tanımlayıp DI da kullanırız
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        // CQRS ve Mediatr ile clean code
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        // IMiddleware kullandığında service eklenmesi gerekiyor
        services.AddTransient<ExceptionMiddleware>();
    }
}
