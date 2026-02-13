namespace NetCoreBackend.Application.Models.Response
{
    public class ProfileResponse : BaseResponseModel
    {
        public ProfileData data { get; set; } = new ProfileData();
    }
    public class ProfileData
    {
        public string Username { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
}