using RestaurantMenu.Application.DTOs;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace RestaurantMenu.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _categoryRepository.GetAllWithMenuItemsAsync();
            return categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                TotalMenuItems = c.MenuItems.Count


            });


        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _categoryRepository.GetByIdWithMenuItemsAsync(id);
            if (category == null)
            {
                return null;
            }
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                TotalMenuItems = category.MenuItems.Count
            };
        }


        public async Task<CategoryDto> CreateAsync(CreateCategoryDto dto)
        {

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                throw new ArgumentException("Category name is required");
            }

            var category = new Category
            {
                Name = dto.Name
            };

            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                TotalMenuItems = 0
            };

        }


        public async Task<bool> UpdateAsync(int id, UpdateCategoryDto dto)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }

            category.Name = dto.Name;

            _categoryRepository.Update(category);
            await _categoryRepository.SaveChangesAsync();
            return true;





        }


        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
            {
                return false;
            }
            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();
            return true;






        }

    }
    
}
