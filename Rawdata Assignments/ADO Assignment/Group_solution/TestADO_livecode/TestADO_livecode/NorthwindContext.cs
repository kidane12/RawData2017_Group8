﻿using System.IO;
using System.Linq;
using DBMapper.DBObjects;
using Microsoft.EntityFrameworkCore;

namespace DBMapper
{
    public class NorthwindContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            var lines = File.ReadLines("credentials.txt").ToArray(); //Use credential due to different passwords - first line <username> second line <password>
            optionsBuilder.UseMySql("server=localhost;database=northwind;uid="+lines[0]+";pwd="+lines[1]+";");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Category
            modelBuilder.Entity<Category>().Property(x => x.Id).HasColumnName("categoryid");
            modelBuilder.Entity<Category>().Property(x => x.Name).HasColumnName("categoryname");

            //Order
            modelBuilder.Entity<Order>().Property(x => x.Id).HasColumnName("orderid");
            modelBuilder.Entity<Order>().Property(x => x.Date).HasColumnName("orderdate");
            modelBuilder.Entity<Order>().Property(x => x.Required).HasColumnName("requireddate");
            modelBuilder.Entity<Order>().Property(x => x.Shipped).HasColumnName("shippeddate");

            //Product
            modelBuilder.Entity<Product>().Property(x => x.Id).HasColumnName("productid");
            modelBuilder.Entity<Product>().Property(x => x.Name).HasColumnName("productname");
            modelBuilder.Entity<Product>().Property(x => x.QuantityPerUnit).HasColumnName("quantityunit");

            //OrderDetails
            modelBuilder.Entity<OrderDetails>().Property(x => x.OrderId1).HasColumnName("orderid");
        }
    }
}
