using System.Reflection;
using Application.Exceptions;
using Application.Interface.AutoMapper;
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
    }
}