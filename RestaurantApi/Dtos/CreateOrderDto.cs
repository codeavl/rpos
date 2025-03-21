namespace RestaurantApi.Dtos
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        public List<OrderItemDto> Items { get; set; } = new();
    }
}
