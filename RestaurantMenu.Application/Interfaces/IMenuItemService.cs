using RestaurantMenu.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.Interfaces
{
    public interface IMenuItemService
    {
        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto?> GetByIdAsync(int id);
        Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto);
        Task<bool> UpdateAsync(int id, UpdateMenuItemDto dto);
        Task<bool> DeleteAsync(int id);

    }
}
