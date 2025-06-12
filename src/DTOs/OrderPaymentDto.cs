namespace MyApi.DTOs;

public class OrderPaymentDto
{
    public long PaymentID { get; set; }
    public long OrderID { get; set; }
    public decimal PaidAmount { get; set; }
    public DateTime? PaymentTime { get; set; }
    public string? PaymentMethod { get; set; }
    public string? TransactionID { get; set; }
    public string? Gateway { get; set; }
    public string? Status { get; set; }
}
