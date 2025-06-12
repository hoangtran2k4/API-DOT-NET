namespace MyApi.DTOs;

public class RequestLogDto
{
    public long RequestID { get; set; }
    public long? OrderID { get; set; }
    public string RequestType { get; set; } = null!;
    public string RequestPayload { get; set; } = null!;
    public string? ResponsePayload { get; set; }
    public string? ResponseCode { get; set; }
    public string? ResponseMessage { get; set; }
    public string Status { get; set; } = "Pending";
    public int RetryCount { get; set; } = 0;
    public bool IsRetryable { get; set; } = true;
    public DateTime CreatedAt { get; set; }
    public DateTime? SentAt { get; set; }
    public DateTime? CompletedAt { get; set; }
    public int? DurationMs { get; set; }
}
