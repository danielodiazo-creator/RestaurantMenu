using RestaurantMenu.Application.DTOs;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application.Services
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IMenuItemRepository _menuItemRepository;

        public MenuItemService(IMenuItemRepository menuItemRepository)
        {
            _menuItemRepository = menuItemRepository;
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
        {
            var menuItems = await _menuItemRepository.GetAllWithCategoryAsync();

            return menuItems.Select(item => new MenuItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.isAvailable,
                CategoryName = item.Category != null ? item.Category.Name : string.Empty
            });


        }

        public async Task<MenuItemDto?> GetByIdAsync(int id)
        {
            var item = await _menuItemRepository.GetByIdWithCategoryAsync(id);
            if (item == null)
            {
                return null;
            }
            return new MenuItemDto
            {
                Id = item.Id,
                Name = item.Name,
                Description = item.Description,
                Price = item.Price,
                IsAvailable = item.isAvailable,
                CategoryName = item.Category != null ? item.Category.Name : string.Empty
            };

        }


        public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Menu item name cannot be empty.");
            }

            if (dto.Price < 0)
            {
                throw new ArgumentException("Price cannot be negative.");
            }

            var menuItem = new MenuItem
            {
                Name = dto.Name,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                isAvailable = true
            };

            await _menuItemRepository.AddAsync(menuItem);
            await _menuItemRepository.SaveChangesAsync();

            return new MenuItemDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name,
                Description = menuItem.Description,
                Price = menuItem.Price,
                IsAvailable = menuItem.isAvailable,
                CategoryName = string.Empty
            };

        }

        public async Task<bool> UpdateAsync(int id, UpdateMenuItemDto dto)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);

            if (menuItem == null)
            {
                return false;
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Menu item name cannot be empty.");
            }

            if (dto.Price <= 0)
            {
                throw new ArgumentException("Price must be greater than zero.");
            }

            menuItem.Name = dto.Name;
            menuItem.Description = dto.Description;
            menuItem.Price = dto.Price;
            menuItem.isAvailable = dto.IsAvailable;
            menuItem.CategoryId = dto.CategoryId;

            _menuItemRepository.Update(menuItem);
            await _menuItemRepository.SaveChangesAsync();

            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var menuItem = await _menuItemRepository.GetByIdAsync(id);
            if (menuItem == null)
            {
                return false;
            }
            _menuItemRepository.Delete(menuItem);
            await _menuItemRepository.SaveChangesAsync();
            return true;


        }

    }
}
