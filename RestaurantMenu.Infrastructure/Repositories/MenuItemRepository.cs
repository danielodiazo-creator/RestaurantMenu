using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Domain.Entities;
using RestaurantMenu.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Infrastructure.Repositories
{
    public class MenuItemRepository : GenericRepository<MenuItem>, IMenuItemRepository
    {
        public MenuItemRepository(AppDbContext context) : base(context) { }

        public async Task<IEnumerable<MenuItem>> GetAllWithCategoryAsync()
        {
            return await _context.MenuItems
                .Include(menuItem => menuItem.Category).ToListAsync();
        }

        public async Task<MenuItem?> GetByIdWithCategoryAsync(int id)
        {
            return await _context.MenuItems
                .Include(menuItem => menuItem.Category)
                .FirstOrDefaultAsync(menuItem => menuItem.Id == id);
        }
    }
}
