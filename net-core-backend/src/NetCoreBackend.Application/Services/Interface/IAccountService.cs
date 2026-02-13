using NetCoreBackend.Application.Models.Response;
using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Application.Services.Service
{
    public interface IAccountService
    {
        Task<CreateAccountResponse> CreateNewAccount(string username, string password);
    }
}