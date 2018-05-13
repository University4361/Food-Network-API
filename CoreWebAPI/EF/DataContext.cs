using CoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace CoreWebAPI.EF
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Courier> Courier { get; set; }
        public DbSet<CourierToken> CourierToken { get; set; }
        public DbSet<Order> Order { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<Customer> Customer { get; set; }
        public DbSet<Review> Review { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Product> Product { get; set; }
        public DbSet<ProductOrder> ProductOrder { get; set; }

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
