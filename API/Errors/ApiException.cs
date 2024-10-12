namespace API.Errors;

public class ApiException(int statusCode, string message, string? details)
{
    public int statuscode { get; set; } = statusCode;
    public string message { get; set; } = message;
    public string? Details { get; set; }  = details;
}