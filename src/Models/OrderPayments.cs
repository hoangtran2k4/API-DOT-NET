using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("OrderPayments", Schema = "OMS")]
    public class OrderPayments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long PaymentID { get; set; }

        [ForeignKey("Order")]
        public long OrderID { get; set; }

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal PaidAmount { get; set; }

        public DateTime? PaymentTime { get; set; }

        [MaxLength(30)]
        public string? PaymentMethod { get; set; }

        [MaxLength(100)]
        public string? TransactionID { get; set; }

        [MaxLength(50)]
        public string? Gateway { get; set; }

        [MaxLength(20)]
        public string? Status { get; set; }

        public virtual Order? Order { get; set; }
    }
}
