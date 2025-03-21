namespace RestaurantApi.Dtos
{
    public class OrderItemResponseDto
    {
        public int MenuItemId { get; set; }
        public string MenuItemName { get; set; } = string.Empty;
        public int Quantity { get; set; }
    }
}
