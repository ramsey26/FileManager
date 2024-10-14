namespace Application.Core
{
    public class AppException
    {
         public AppException(int statusCode, string Message, string details = null)
        {
            this.StatusCode = statusCode;
            this.Message = Message;
            this.Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}