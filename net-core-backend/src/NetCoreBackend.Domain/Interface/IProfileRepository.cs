using NetCoreBackend.Domain.Entities;

namespace NetCoreBackend.Domain.Interface
{
    public interface IProfileRepository
    {
        Task<ProfileEntity?> GetProfileByUsername(string username);
    }
}