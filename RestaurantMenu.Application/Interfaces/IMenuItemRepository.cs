using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.Interfaces
{
    public interface IMenuItemRepository : IGenericRepository<MenuItem>
    {
        Task<IEnumerable<MenuItem>> GetAllWithCategoryAsync();
        Task<MenuItem?> GetByIdWithCategoryAsync(int id);
    }
}
