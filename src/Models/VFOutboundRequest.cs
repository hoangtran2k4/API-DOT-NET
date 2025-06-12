using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("RequestLogs", Schema = "OMS")]
    public class RequestLog
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long RequestID { get; set; }

        [ForeignKey("Order")]
        public long? OrderID { get; set; }

        [Required]
        [MaxLength(50)]
        public string RequestType { get; set; } = null!;

        [Required]
        public string RequestPayload { get; set; } = null!;

        public string? ResponsePayload { get; set; }

        [MaxLength(20)]
        public string? ResponseCode { get; set; }

        [MaxLength(500)]
        public string? ResponseMessage { get; set; }

        [MaxLength(20)]
        public string Status { get; set; } = "Pending";

        public int RetryCount { get; set; } = 0;

        public bool IsRetryable { get; set; } = true;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public DateTime? SentAt { get; set; }

        public DateTime? CompletedAt { get; set; }

        public int? DurationMs { get; set; }

        public virtual Order? Order { get; set; }
    }
}
