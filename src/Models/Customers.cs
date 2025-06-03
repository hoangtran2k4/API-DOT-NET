using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("Customers", Schema = "OMS")]
    public class Customer
    {
        [Key]
        public long CustomerID { get; set; }
        [MaxLength(100)]
        public string FullName { get; set; } = null!;
         [MaxLength(100)]
        public string? Email { get; set; }
         [MaxLength(20)]
        public string Phone { get; set; } = null!;
        public char? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
         [MaxLength(30)]
        public string? Channel { get; set; }
        public bool IsMember { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? UpdatedAt { get; set; }
    }
}
