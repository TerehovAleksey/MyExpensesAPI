namespace MyExpensesAPI.Models
{
    public class ErrorDetails
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }

        public static ErrorDetails Create(int statusCode, string message)
        {
            return new() { Message = message, StatusCode = statusCode };
        }
    }
}
