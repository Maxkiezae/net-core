using NetCoreBackend.Application.Services.Interface;
using NetCoreBackend.Domain.Entities;
using NetCoreBackend.Domain.Interface;

namespace NetCoreBackend.Application.Services.Service
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _profileRepository;

        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }

        public async Task<ProfileEntity> GetProfileByUsername(string username)
        {
            var data = await _profileRepository.GetProfileByUsername(username);
            if (data == null)
            {
                throw new Exception("Profile not found");
            }
            return data;
        }
    }
}