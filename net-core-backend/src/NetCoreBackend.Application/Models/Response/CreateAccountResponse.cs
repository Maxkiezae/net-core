namespace NetCoreBackend.Application.Models.Response
{
    public class CreateAccountResponse : BaseResponseModel
    {
        public CreateAccountResponseData Data { get; set; } = new();
    }
    public class CreateAccountResponseData
    {
        public string username { get; set; } = string.Empty;
    }
}