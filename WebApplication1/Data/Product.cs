using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    [Table("Product")]
    public class Product
    {
        [Key]
        public Guid productId { get; set; }
        [Required]
        [MaxLength(100)]
        public string productName { get; set; }
        public string? productDescription { get; set; }
        [Range(0, double.MaxValue)]
        public double productUnitPrice {  get; set; }
        public byte productSale { get; set; }
        public int? productCategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }

        public Product()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
