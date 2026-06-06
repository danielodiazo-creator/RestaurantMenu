using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.DTOs
{
    public class CreateMenuItemDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int CategoryId { get; set; }




    }
}
