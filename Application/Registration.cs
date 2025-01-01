using System.Reflection;
using Application.Bases;
using Application.Exceptions;
using Application.Interface.AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        // MediatR servisini ekle
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

        // ExceptionMiddleware servisini ekle
        services.AddTransient<ExceptionMiddleware>();

        // Custom Mapper servisini ekle
        services.AddSingleton<ICustomMapper, Application.Mapper.AutoMapper.Mapper>();

        // FluentValidation validatörlerini ekle
        services.AddValidatorsFromAssembly(assembly);
        ValidatorOptions.Global.LanguageManager.Culture = new System.Globalization.CultureInfo("tr");
        
        // FluentValidation Behavior'ı MediatR Pipeline'a ekle
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(Behaviors.FluentValidationBehavior<,>));

        // Custom Rulelları ekle
        services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));
    }

    public static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
        Assembly assembly, Type rulesType )
    {
        var types = assembly.GetTypes().Where(t => t.IsClass && 
                                                   !t.IsAbstract && rulesType.IsAssignableFrom(t));

        foreach (var rule in types)
        {
            services.AddTransient(rule);
        }
        return services;
    }
}