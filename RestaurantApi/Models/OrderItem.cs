using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantApi.Models
{
    public class OrderItem
    {
        public int Id { get; set; }

        [ForeignKey("Order")]
        public int OrderId { get; set; }
        public Order Order { get; set; }

        [ForeignKey("MenuItem")]
        public int MenuItemId { get; set; }
        public MenuItem MenuItem { get; set; } = new();

        public int Quantity { get; set; } = 1;
    }
}
