using Microsoft.Extensions.DependencyInjection;
using RestaurantMenu.Application.Interfaces;
using RestaurantMenu.Application.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IMenuItemService, MenuItemService>();
            return services;
        }



    }
}
