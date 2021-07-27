using Microsoft.EntityFrameworkCore;
using StoreAppModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAppDL
{
    public class StoreAppDBContext : DbContext
    {
        public StoreAppDBContext() : base()
        { }

        public StoreAppDBContext(DbContextOptions options) : base(options)
        { }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<LineItem> LineItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Manager> Managers { get; set; }
        public DbSet<StoreFront> StoreFronts { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder p_modelBuilder)
        {
            p_modelBuilder.Entity<Customer>()
                .Property(cust => cust.CustomerID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Order>()
                .Property(ord => ord.OrderID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<LineItem>()
                .Property(li => li.LineItemID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Product>()
                .Property(prod => prod.ProductID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<StoreFront>()
                .Property(sf => sf.StoreFrontID)
                .ValueGeneratedOnAdd();

            p_modelBuilder.Entity<Manager>()
                .Property(man => man.ManagerID)
                .ValueGeneratedOnAdd();
        }
    }
}
