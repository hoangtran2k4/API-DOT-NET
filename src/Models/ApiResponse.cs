namespace MyApi.Models;

public class ApiResponse<T>
{
    public bool Success { get; set; }
    public int HttpStatusCode { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public int TotalCount { get; set; }
    public string? ErrorCode { get; set; }
}
