using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Infa.Db.SqlServer.EfCore.EntityConfigurations;

public class ProductAttributeValueConfigs : IEntityTypeConfiguration<ProductAttributeValue>
{
    public void Configure(EntityTypeBuilder<ProductAttributeValue> builder)
    {
        builder.ToTable("ProductAttributeValues");
        builder.HasKey(av => av.Id);

        builder.Property(av => av.Value)
            .IsRequired()
            .HasMaxLength(200);

        builder.Property(c => c.CreatedAt)
            .HasDefaultValueSql("GetDate()")
            .ValueGeneratedOnAdd();

        builder.HasOne(av => av.Product)
            .WithMany(p => p.AttributeValues)
            .HasForeignKey(av => av.ProductId)
            .OnDelete(DeleteBehavior.NoAction);

        builder.HasOne(av => av.ProductAttribute)
            .WithMany(pa => pa.AttributeValues)
            .HasForeignKey(av => av.ProductAttributeId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}