using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        Task<IEnumerable<Category>> GetAllWithMenuItemsAsync();
        Task<Category?> GetByIdWithMenuItemsAsync(int id);


    }
}
