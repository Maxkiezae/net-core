using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Application.Services.Interface
{
    public interface IProfileService
    {
        Task<ProfileEntity> GetProfileByUsername(string username);
    }
}