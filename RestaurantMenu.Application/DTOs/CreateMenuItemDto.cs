using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.DTOs
{
    public class CreateMenuItemDto
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int CategoryId { get; set; }




    }
}
