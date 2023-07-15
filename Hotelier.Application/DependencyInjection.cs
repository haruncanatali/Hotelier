using System.Reflection;
using Hotelier.Application.Common.Interfaces;
using Hotelier.Application.Common.Managers;
using Hotelier.Application.Common.Mappings;
using Microsoft.Extensions.DependencyInjection;

namespace Hotelier.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddOptions();
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MappingProfile).Assembly));
        services.AddTransient<IFileService, FileManager>();
        return services;
    }
}