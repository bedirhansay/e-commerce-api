using Application.Interface;
using Application.Interface.Repositories;
using Infrastructure.Context;
using Infrastructure.Repositories;
using Infrastructure.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Burası Dependency injection kısmı 
namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new Exception("Connection string 'DefaultConnection' is missing or empty.");
        }
        services.AddDbContext<AppDbContext>(opt => opt.UseNpgsql(connectionString));
        services.AddDbContext<AppDbContext>(opt=>opt.UseNpgsql(connectionString));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped< IUnitOfWork,UnitOfWork>();
        return services;
    
    }
 
}
