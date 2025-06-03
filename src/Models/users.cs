using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApi.Models
{
    [Table("Users", Schema = "OMS")]
    public class Users
    {
        [Key]
        public string id { get; set; } = null!;
        [MaxLength(100)]
        public string username { get; set; } = null!;
        [MaxLength(100)]
        public string password { get; set; } = null!;

        [MaxLength(20)]
        public string role { get; set; } = null!;
    }
}
