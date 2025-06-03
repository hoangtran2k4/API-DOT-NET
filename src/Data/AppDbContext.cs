// Data/AppDbContext.cs
// DbContext khai báo DbSet cho model Product
using Microsoft.EntityFrameworkCore;
using MyApi.Models;

namespace MyApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products { get; set; }

    public DbSet<Customer> Customers { get; set; }

    public DbSet<Users> Users { get; set; }
}

