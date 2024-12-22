using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using MediatR;

namespace Application;

public static class Registration
{
    public static void AddApplication(IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
    }
}