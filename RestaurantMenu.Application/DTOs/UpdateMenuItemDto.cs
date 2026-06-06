using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.DTOs
{
    public class UpdateMenuItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public bool IsAvailable { get; set; }
        public int CategoryId { get; set; }



    }
}
