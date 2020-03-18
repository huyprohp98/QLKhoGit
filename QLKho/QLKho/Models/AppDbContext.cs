using Microsoft.EntityFrameworkCore;
using QLKho.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DemoInventory.API.Models
{
    public class AppDbContext : DbContext
    {
        public DbSet<Inventory> Inventory { get; set; } /// <summary> chinh la tao cac bang cua minh

        /// </summary>

        //public object Customer { get; internal set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Unit> Unit { set; get; }
        public DbSet<Stock> Stock { set; get; }
        public DbSet<Issue> Issue { set; get; }
        public DbSet<Staff> Staff { set; get; }
        public DbSet<Receipt> Receipt { set; get; }

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder) /*ham tao database tu code*/
        {
            base.OnModelCreating(builder);

            builder.Entity<Inventory>().ToTable("Inventory");
            builder.Entity<Inventory>().HasKey(p => p.Id);
            builder.Entity<Inventory>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Inventory>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().Property(p => p.Price).IsRequired().HasMaxLength(30);
            builder.Entity<Inventory>().Property(p => p.Amount).IsRequired() ;
            builder.Entity<Inventory>().HasMany(p => p.Issue).WithOne(p => p.Inventory).HasForeignKey(p => p.InventoryId);
            builder.Entity<Inventory>().HasMany(p => p.Receipt).WithOne(p => p.Inventory).HasForeignKey(p => p.InventoryId);
            //builder.Entity<Category>().HasData
            //(
            //    new Category { Id = 100, Name = "Fruits and Vegetables" }, // Id set manually due to in-memory provider
            //    new Category { Id = 101, Name = "Dairy" }
            //);


            //Customer
            builder.Entity<Customer>().ToTable("Customer");
            builder.Entity<Customer>().HasKey(p => p.Id);
            builder.Entity<Customer>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Customer>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().Property(p => p.Address).IsRequired().HasMaxLength(30);
            builder.Entity<Customer>().Property(p => p.Phone).IsRequired();
            builder.Entity<Customer>().HasMany(p => p.Issue).WithOne(p => p.Customer).HasForeignKey(p => p.CustomerId);
            //Unit
            builder.Entity<Unit>().ToTable("Unit");
            builder.Entity<Unit>().HasKey(p => p.Id);
            builder.Entity<Unit>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Unit>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Unit>().HasMany(p => p.Inventory).WithOne(p => p.Unit).HasForeignKey(p => p.UnitId);
          
            //Stock
            builder.Entity<Stock>().ToTable("Stock");
            builder.Entity<Stock>().HasKey(p => p.Id);
            builder.Entity<Stock>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Stock>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Stock>().Property(p => p.Address).IsRequired().HasMaxLength(30);
            builder.Entity<Stock>().HasMany(p => p.Inventory).WithOne(p => p.Stock).HasForeignKey(p => p.StockId);
          
            //Issue
            builder.Entity<Issue>().ToTable("Issue");
            builder.Entity<Issue>().HasKey(p => p.Id);
            builder.Entity<Issue>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Issue>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Issue>().Property(p => p.Creatdate).IsRequired();
            builder.Entity<Issue>().Property(p => p.Amount).IsRequired();
            builder.Entity<Issue>().Property(p => p.Price).IsRequired().HasMaxLength(30);
            builder.Entity<Issue>().Property(p => p.Content).IsRequired().HasMaxLength(150);
            //Receipt
            builder.Entity<Receipt>().ToTable("Receipt");
            builder.Entity<Receipt>().HasKey(p => p.Id);
            builder.Entity<Receipt>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Receipt>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Creatdate).IsRequired();
            builder.Entity<Receipt>().Property(p => p.Amount).IsRequired();
            builder.Entity<Receipt>().Property(p => p.Price).IsRequired().HasMaxLength(30);
            builder.Entity<Receipt>().Property(p => p.Content).IsRequired().HasMaxLength(150);
            //Staff
            builder.Entity<Staff>().ToTable("Staff");
            builder.Entity<Staff>().HasKey(p => p.Id);
            builder.Entity<Staff>().Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Entity<Staff>().Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.Entity<Staff>().Property(p => p.Address).IsRequired().HasMaxLength(30);
            builder.Entity<Staff>().Property(p => p.Phone).IsRequired();
            builder.Entity<Staff>().HasMany(p => p.Receipt).WithOne(p => p.Staff).HasForeignKey(p => p.StaffId);

        }
    }
}
