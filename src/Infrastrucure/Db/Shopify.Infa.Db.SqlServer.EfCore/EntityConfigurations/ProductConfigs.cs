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
    }
}