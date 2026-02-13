namespace NetCoreBackend.Application.Models.Request
{
    public class CreateAccountRequest
    {
        public string username { get; set; } = string.Empty;
        public string password { get; set; } = string.Empty;
    }
}