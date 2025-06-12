using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("Orders", Schema = "OMS")]
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long OrderID { get; set; }

        [Required]
        [MaxLength(50)]
        public string OrderCode { get; set; } = null!;
        [ForeignKey("Customer")]
        public long CustomerID { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        [MaxLength(20)]
        public string Status { get; set; } = null!;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalAmount { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountAmount { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal ShippingFee { get; set; } = 0;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TaxAmount { get; set; } = 0;

        [NotMapped]
        public decimal FinalAmount => TotalAmount - DiscountAmount + ShippingFee + TaxAmount;
        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        [MaxLength(20)]
        public string? PaymentStatus { get; set; }

        [MaxLength(30)]
        public string? Channel { get; set; }

        [ForeignKey("Warehouse")]
        public int WarehouseID { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? UpdatedAt { get; set; }

        public virtual Customer? Customer { get; set; }
        public virtual Warehouse? Warehouse { get; set; }
    }
}
