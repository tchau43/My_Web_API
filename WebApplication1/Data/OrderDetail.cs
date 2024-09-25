namespace WebApplication1.Data
{
    public class OrderDetail
    {
        public int orderId { get; set; }
        public Guid productId { get; set; }
        public int quantity { get; set; }
        public double unitPrice {  get; set; }
        public byte sale {  get; set; }


        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
