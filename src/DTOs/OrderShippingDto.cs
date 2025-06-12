namespace MyApi.DTOs;

public class OrderShippingDto
{
    public long OrderID { get; set; }
    public string ReceiverName { get; set; } = null!;
    public string? Phone { get; set; }
    public string? AddressLine1 { get; set; }
    public string? AddressLine2 { get; set; }
    public string? Ward { get; set; }
    public string? District { get; set; }
    public string? Province { get; set; }
    public string? PostalCode { get; set; }
    public string? ShippingStatus { get; set; }
    public string? ShippingPartner { get; set; }
    public string? TrackingCode { get; set; }
    public DateTime EstimatedDate { get; set; }
    public DateTime? DeliveredDate { get; set; }
}
