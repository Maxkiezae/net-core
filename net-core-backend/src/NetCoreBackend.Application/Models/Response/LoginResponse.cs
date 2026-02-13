namespace NetCoreBackend.Application.Models.Response
{
    public class LoginResponse : BaseResponseModel
    {
        public LoginData Data { get; set; } = new LoginData();
    }

    public class LoginData
    {
        public string token { get; set; } = string.Empty;
        public DateTime? ExpireDate { get; set; } = new DateTime();
    }
}