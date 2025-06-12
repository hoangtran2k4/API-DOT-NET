namespace MyApi.DTOs;

public class OrderItemDto
{
    public long OrderItemID { get; set; }
    public long OrderID { get; set; }
    public long ProductID { get; set; }
    public string SKU { get; set; } = null!;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal Discount { get; set; }
    public decimal TotalPrice { get; set; }
}
