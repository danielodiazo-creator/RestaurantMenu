using FluentAssertions;
using NSubstitute;
using RestaurantMenu.Application.DTOs;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Application.Services;
using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Tests
{
    public class CategoryServiceTests
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly CategoryService _categoryService;

        public CategoryServiceTests()
        {
            _categoryRepository = Substitute.For<ICategoryRepository>();
            _categoryService = new CategoryService(_categoryRepository);
        }

        [Fact]
        public async Task GetAllAsync_WhenCategoriesExist_ReturnsCategoryDtos()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category
                {
                    Id = 1,
                    Name = "Drinks",
                    MenuItems = new List<MenuItem>
                    {
                        new MenuItem { Id = 1, Name = "Coca Cola" }
                    }
                },
                new Category
                {
                    Id = 2,
                    Name = "Food",
                    MenuItems = new List<MenuItem>()
                }
            };

            _categoryRepository.GetAllWithMenuItemsAsync()
                .Returns(categories);

            // Act
            var result = await _categoryService.GetAllAsync();

            // Assert
            result.Should().HaveCount(2);
            result.First().Name.Should().Be("Drinks");
            result.First().TotalMenuItems.Should().Be(1);
        }

        [Fact]
        public async Task GetByIdAsync_WhenCategoryExists_ReturnsCategoryDto()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Drinks",
                MenuItems = new List<MenuItem>()
            };

            _categoryRepository.GetByIdWithMenuItemsAsync(1)
                .Returns(category);

            // Act
            var result = await _categoryService.GetByIdAsync(1);

            // Assert
            result.Should().NotBeNull();
            result!.Id.Should().Be(1);
            result.Name.Should().Be("Drinks");
        }

        [Fact]
        public async Task GetByIdAsync_WhenCategoryDoesNotExist_ReturnsNull()
        {
            // Arrange
            _categoryRepository.GetByIdWithMenuItemsAsync(99)
                .Returns((Category?)null);

            // Act
            var result = await _categoryService.GetByIdAsync(99);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task CreateAsync_WhenNameIsValid_ReturnsCreatedCategoryDto()
        {
            // Arrange
            var dto = new CreateCategoryDto
            {
                Name = "Desserts"
            };

            // Act
            var result = await _categoryService.CreateAsync(dto);

            // Assert
            result.Name.Should().Be("Desserts");
            result.TotalMenuItems.Should().Be(0);

            await _categoryRepository.Received(1).AddAsync(
                Arg.Is<Category>(category => category.Name == "Desserts"));

            await _categoryRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task CreateAsync_WhenNameIsEmpty_ThrowsArgumentException()
        {
            // Arrange
            var dto = new CreateCategoryDto
            {
                Name = ""
            };

            // Act
            var act = async () => await _categoryService.CreateAsync(dto);

            // Assert
            await act.Should().ThrowAsync<ArgumentException>()
                .WithMessage("Category name is required");
        }

        [Fact]
        public async Task UpdateAsync_WhenCategoryExists_ReturnsTrue()
        {
            // Arrange
            var category = new Category
            {
                Id = 1,
                Name = "Old Name"
            };

            var dto = new UpdateCategoryDto
            {
                Name = "New Name"
            };

            _categoryRepository.GetByIdAsync(1)
                .Returns(category);

            // Act
            var result = await _categoryService.UpdateAsync(1, dto);

            // Assert
            result.Should().BeTrue();
            category.Name.Should().Be("New Name");

            _categoryRepository.Received(1).Update(category);
            await _categoryRepository.Received(1).SaveChangesAsync();
        }

        [Fact]
        public async Task DeleteAsync_WhenCategoryDoesNotExist_ReturnsFalse()
        {
            // Arrange
            _categoryRepository.GetByIdAsync(99)
                .Returns((Category?)null);

            // Act
            var result = await _categoryService.DeleteAsync(99);

            // Assert
            result.Should().BeFalse();
            _categoryRepository.DidNotReceive().Delete(Arg.Any<Category>());
        }
    }
}
