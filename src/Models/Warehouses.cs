using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("Warehouses", Schema = "Inventory")]
    public class Warehouse
    {
        [Key]
        public int WarehouseID { get; set; }

        [MaxLength(100)]
        public string Name { get; set; } = null!;

        [MaxLength(100)]
        public string? Province { get; set; }

        [MaxLength(50)]
        public int Capacity { get; set; }

        public bool IsActive { get; set; }

    }
}