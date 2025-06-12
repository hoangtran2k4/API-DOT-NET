using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Users> Users { get; set; }

        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderPayments> OrderPayments { get; set; }
        public DbSet<OrderShipping> OrderShipping { get; set; }
        public DbSet<RequestLog> RequestLogs { get; set; }
        public DbSet<OrderShipping> OrderShippings { get; set; }
        public DbSet<Warehouse> Warehouses { get; set; } = null!;

    }
}
