using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Domain.Entities
{
    public class Category
    {
        public int Id { get; set; } 
        public String Name { get; set; }
        public ICollection<MenuItem> MenuItems { get; set; } = new List<MenuItem>();

    }
}
