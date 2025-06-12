namespace MyApi.DTOs
{
    public class WarehouseDto
    {
        public int WarehouseID { get; set; }
        public string Name { get; set; } = null!;
        public string? Province { get; set; }
        public int Capacity { get; set; }
        public bool IsActive { get; set; }
    }
}
