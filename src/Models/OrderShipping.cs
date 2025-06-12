using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("OrderShipping", Schema = "OMS")]
    public class OrderShipping
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long ShippingID { get; set; }

        [ForeignKey("Order")]
        public long OrderID { get; set; }

        [MaxLength(100)]
        public string ReceiverName { get; set; } = null!;

        [MaxLength(20)]
        public string? Phone { get; set; }

        [MaxLength(200)]
        public string? AddressLine1 { get; set; }

        [MaxLength(200)]
        public string? AddressLine2 { get; set; }

        [MaxLength(100)]
        public string? Ward { get; set; }

        [MaxLength(100)]
        public string? District { get; set; }
        [MaxLength(100)]
        public string? Province { get; set; }
        [MaxLength(10)]
        public string? PostalCode { get; set; }
        [MaxLength(20)]
        public string? ShippingStatus { get; set; }
        [MaxLength(50)]
        public string? ShippingPartner { get; set; }
        [MaxLength(100)]
        public string? TrackingCode { get; set; }

        public DateTime EstimatedDate { get; set; }

        public DateTime? DeliveredDate { get; set; }
    }
}