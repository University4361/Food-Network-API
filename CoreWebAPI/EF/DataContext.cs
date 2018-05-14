using CoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Courier> Couriers { get; set; }
        public DbSet<CourierToken> CourierTokens { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Review> Reviews { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOrder>()
                .HasKey(c => new { c.ProductID, c.OrderID });

            modelBuilder.Entity<ProductOrder>()
                .Property(b => b.Quantity)
                .HasDefaultValue(1);
        }
    }
}
