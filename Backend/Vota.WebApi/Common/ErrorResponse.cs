namespace Vota.WebApi.Common
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public string ErrorCode { get; set; }
        public object Details { get; set; }
    }
}
