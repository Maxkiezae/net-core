namespace NetCoreBackend.Application.Models
{
    public class BaseResponseModel
    {
        public int StatusCode { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
