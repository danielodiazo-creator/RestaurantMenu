using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Infrastructure.Data;
using RestaurantMenu.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            // Register repositories
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IMenuItemRepository, MenuItemRepository>();
            return services;
        }

    }
}
