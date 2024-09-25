namespace WebApplication1.Models
{
    public class ProductsVM
    {
        public string productName {  get; set; }
        public double productUnitPrice { get; set; }
        public string productCategory { get; set; }
    }

    public class ProductModel : ProductsVM
    {
        public Guid productId { get; set; }
    }

    public class IProductModel
    {
        public Guid productId { get; set; }
        public string? productName { get; set; }
        public double productUnitPrice { get; set; }
        public string productCategory {  get; set; }
    }
}
