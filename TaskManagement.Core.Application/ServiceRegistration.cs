using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace TaskManagement.Core.Application;

public static class ServiceRegistration
{
    public static void AddApplicationLayer(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(config => config.RegisterServicesFromAssemblies(
            Assembly.GetExecutingAssembly()));
    }
}