using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    [Table("User")]
    public class User
    {
        [Key]
        public int userId { get; set; }
        [Required]
        [MaxLength(50)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        [PasswordPropertyText]
        public string userPassword { get; set; }
        public string userFullName { get; set; }
        [EmailAddress]
        public string userEmail { get; set; }
    }
}
