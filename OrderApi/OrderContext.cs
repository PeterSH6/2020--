using Microsoft.EntityFrameworkCore;

namespace OrderApi.Models
{
    public class OrderContext : DbContext{
        public OrderContext(DbContextOptions<OrderContext> options) : base(options)
        {
            this.Database.EnsureCreated();//自动建库建表
        }
        //EFCore使用复合主键建议使用FluentAPI
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<OrderItem>().HasKey(c => new{c.ItemID,c.OrderID});
        }

        public DbSet<Order> Orders {get;set;}
        public DbSet<OrderItem> OrderItems{get;set;}
    }
}