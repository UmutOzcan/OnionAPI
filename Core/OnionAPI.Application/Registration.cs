using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnionAPI.Application.Behaviours;
using OnionAPI.Application.Exceptions;
using System.Globalization;
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

        // FluentValidation service eklemesi
        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");

        // her çağrıda yeni instance
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviours<,>));
    }
}
