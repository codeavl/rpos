using RestaurantApi.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models
{
    public class Order
    {
        public int Id { get; set; }

        public int TableId { get; set; }
        public Table? Table { get; set; }

        public DateTime OrderTime { get; set; } = DateTime.UtcNow;

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        // 👇 Navigation property to hold list of items
        public List<OrderItem> OrderItems { get; set; } = [];
    }

}
