using Microsoft.Extensions.DependencyInjection;
using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Application.Services.Service;

namespace NetCoreBackend.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<ITodoService, TodoService>();
        services.AddScoped<IAuthenService, AuthenService>();
        services.AddScoped<IProfileService, ProfileService>();
        services.AddScoped<IAccountService, AccountService>();
        return services;
    }
}
