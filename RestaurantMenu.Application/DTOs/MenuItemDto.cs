using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.DTOs
{
    public class MenuItemDto
    {

        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public string CategoryName { get; set; } = string.Empty;
    }
}
