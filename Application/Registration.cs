using System.Reflection;
using Application.Interface.AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class Registration
{
    public static void AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }

    public static void AddCustomMapper(this IServiceCollection services)
    {
        services.AddSingleton<ICustomMapper,Application.Mapper.AutoMapper.Mapper>();
    }
    
}