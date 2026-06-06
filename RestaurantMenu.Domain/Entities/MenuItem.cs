using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Domain.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool isAvailable { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
