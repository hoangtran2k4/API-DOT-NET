using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("OrderItems", Schema = "OMS")]
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderItemID { get; set; }

        [ForeignKey("Order")]
        public long OrderID { get; set; }

        [ForeignKey("Product")]
        public long ProductID { get; set; }

        [Required]
        [MaxLength(50)]
        public string SKU { get; set; } = null!;

        [Required]
        public int Quantity { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Discount { get; set; } = 0;

        [NotMapped] 
        public decimal TotalPrice => (UnitPrice - Discount) * Quantity;
        public virtual Order? Order { get; set; }
        public virtual Product? Product { get; set; }
    }
}
