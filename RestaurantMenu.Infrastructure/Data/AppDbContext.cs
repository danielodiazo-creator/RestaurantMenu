using Microsoft.EntityFrameworkCore;
using RestaurantMenu.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace RestaurantMenu.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Configure the relationships and constraints
            modelBuilder.Entity<Category>()
                .HasMany(category => category.MenuItems)
                .WithOne(menuItem => menuItem.Category)
                .HasForeignKey(menuItem => menuItem.CategoryId);


        }


    }
}
