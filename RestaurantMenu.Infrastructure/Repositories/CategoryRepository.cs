using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Domain.Entities;
using RestaurantMenu.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RestaurantMenu.Infrastructure.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(AppDbContext context) : base(context) { }
        
        public async Task<IEnumerable<Category>> GetAllWithMenuItemsAsync()
        {
            return await _context.Categories
                .Include(category => category.MenuItems).ToListAsync();
        }

        public async Task<Category?> GetByIdWithMenuItemsAsync(int id)
        {
            return await _context.Categories
                .Include(category => category.MenuItems)
                .FirstOrDefaultAsync(category => category.Id == id);
        }
    }
}
