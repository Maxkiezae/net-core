using NetCoreBackend.Application.Models.Request;
using NetCoreBackend.Application.Models.Response;

namespace NetCoreBackend.Application.Services.Interface
{
    public interface IAuthenService
    {
        Task<LoginResponse> Login(LoginRequest request, CancellationToken cancellationToken);
    }
}