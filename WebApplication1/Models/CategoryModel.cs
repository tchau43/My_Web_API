using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class CategoryModel
    {
        [Required]
        [MaxLength(50)]
        public string CategoryName { get; set; }
    }
}
