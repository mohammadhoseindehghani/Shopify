using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class ProductConfigs : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.ToTable("Products");

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Title)
            .IsRequired()

            .HasMaxLength(200);
        builder.Property(p => p.ShortDescription)
            .HasMaxLength(500);

        builder.Property(p => p.ImageUrl)
            .HasMaxLength(300);

        builder.Property(p => p.Price)
            .HasColumnType("decimal(18,0)")
            .IsRequired();

        builder.Property(p => p.StockQuantity)
            .IsRequired();

        builder.Property(p => p.IsActive)
            .IsRequired();

        builder.Property(p => p.IsSpecial)
            .IsRequired();

        builder.HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();


        builder.HasMany(p => p.AttributeValues)
            .WithOne(av => av.Product)
            .HasForeignKey(av => av.ProductId)
            .OnDelete(DeleteBehavior.NoAction);


        builder.HasData(
    new Product
    {
        Id = 1,
        Title = "iPhone 14 Pro",
        ShortDescription = "Apple flagship smartphone with A16 chip",
        ImageUrl = "iphone14pro.jpg",
        Price = 120000000,
        StockQuantity = 12,
        IsActive = true,
        IsSpecial = true,
        CategoryId = 3,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 2,
        Title = "Samsung Galaxy S23",
        ShortDescription = "Latest Samsung flagship with Snapdragon processor",
        ImageUrl = "s23.jpg",
        Price = 95000000,
        StockQuantity = 20,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 3,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 3,
        Title = "Macbook Pro M2",
        ShortDescription = "Apple Macbook Pro with M2 processor",
        ImageUrl = "macbookm2.jpg",
        Price = 150000000,
        StockQuantity = 8,
        IsActive = true,
        IsSpecial = true,
        CategoryId = 2,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 4,
        Title = "Dell XPS 13",
        ShortDescription = "Lightweight premium ultrabook",
        ImageUrl = "dellxps13.jpg",
        Price = 110000000,
        StockQuantity = 14,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 2,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 5,
        Title = "AirPods Pro 2",
        ShortDescription = "Apple wireless noise-cancelling earbuds",
        ImageUrl = "airpodspro2.jpg",
        Price = 8500000,
        StockQuantity = 50,
        IsActive = true,
        IsSpecial = true,
        CategoryId = 4,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 6,
        Title = "Sony WH-1000XM5",
        ShortDescription = "Sony premium noise-cancelling headphones",
        ImageUrl = "xm5.jpg",
        Price = 12000000,
        StockQuantity = 25,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 4,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 7,
        Title = "Samsung Galaxy Watch 6",
        ShortDescription = "Smart watch with health and fitness tracking",
        ImageUrl = "watch6.jpg",
        Price = 9000000,
        StockQuantity = 30,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 5,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 8,
        Title = "Apple Watch Series 9",
        ShortDescription = "Latest Apple Watch with powerful S9 chip",
        ImageUrl = "applewatch9.jpg",
        Price = 18000000,
        StockQuantity = 18,
        IsActive = true,
        IsSpecial = true,
        CategoryId = 5,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 9,
        Title = "Asus ROG Phone 7",
        ShortDescription = "Gaming smartphone with powerful specs",
        ImageUrl = "rogphone7.jpg",
        Price = 75000000,
        StockQuantity = 10,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 3,
        CreatedAt = DateTime.UtcNow
    },
    new Product
    {
        Id = 10,
        Title = "Xiaomi Redmi Note 12",
        ShortDescription = "Budget friendly smartphone with great features",
        ImageUrl = "redminote12.jpg",
        Price = 12000000,
        StockQuantity = 40,
        IsActive = true,
        IsSpecial = false,
        CategoryId = 3,
        CreatedAt = DateTime.UtcNow
    });

    }
}