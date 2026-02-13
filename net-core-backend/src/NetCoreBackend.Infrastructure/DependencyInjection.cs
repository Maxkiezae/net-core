using Microsoft.Extensions.DependencyInjection;
using NetCoreBackend.Domain.Interface;
using NetCoreBackend.Infrastructure.Persistence;
using NetCoreBackend.Infrastructure.Repositories;

namespace NetCoreBackend.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<ITodoRepository, InMemoryTodoRepository>();
        services.AddSingleton<IUserRepository, UserRepository>();
        services.AddSingleton<IProfileRepository, ProfileRepository>();
        services.AddScoped<IAccountRepository, AccountRepository>();
        return services;
    }
}
