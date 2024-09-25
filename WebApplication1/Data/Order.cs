using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Data
{
    public enum Status
    {
        Success = 0, Paid = 1, Complete = 2, Cancel = -1
    }
    [Table("Order")]
    public class Order
    {
        [Key]
        public int orderId { get; set; }
        public DateTime oderDate { get; set; }
        public DateTime? deliveryDate { get; set; }
        public Status orderStatus { get; set; }
        public string receiver { get; set; }
        public string receiverAddress { get; set; }
        public string receiverPhoneNumber { get; set; }

        public ICollection<OrderDetail> OrderDetails { get; set; }
        public Order()
        {
            OrderDetails = new List<OrderDetail>();
        }
    }
}
