namespace RestaurantApi.Dtos
{
    public class OrderResponseDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public DateTime OrderTime { get; set; }
        public string Status { get; set; } = string.Empty;
        public List<OrderItemResponseDto> Items { get; set; } = [];
    }
}
