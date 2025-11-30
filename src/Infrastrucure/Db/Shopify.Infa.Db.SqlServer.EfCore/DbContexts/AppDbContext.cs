using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.CartAgg.Entities;
using Shopify.Domain.Core.CategoryAgg.Entities;
using Shopify.Domain.Core.OrderAgg.Entities;
using Shopify.Domain.Core.ProductAgg.Entities;
using Shopify.Domain.Core.UserAgg.Entities;
using Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

namespace Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<User> Users { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Cart> Carts { get; set; }
    public DbSet<CartItem> CartItems { get; set; }
    public DbSet<ProductAttribute> ProductAttributes { get; set; }
    public DbSet<ProductAttributeValue> ProductAttributeValues { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfigs).Assembly);
        base.OnModelCreating(modelBuilder);
    }
}