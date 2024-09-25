using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace WebApplication1.Models
{
    public class LoginModel
    {
        [Required]
        [MaxLength(50)]
        public string userName { get; set; }
        [Required]
        [MaxLength(100)]
        [PasswordPropertyText]
        public string userPassword { get; set; }
    }
}
