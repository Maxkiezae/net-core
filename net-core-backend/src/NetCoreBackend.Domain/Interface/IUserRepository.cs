using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Domain.Interface
{
    public interface IUserRepository
    {
        Task<UserEntity?> GetUserByLogin(string Username, string Password);
    }
}
