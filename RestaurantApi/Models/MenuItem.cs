﻿using RestaurantApi.Enums;

namespace RestaurantApi.Models
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public CategoryType Category { get; set; }
    }
}
