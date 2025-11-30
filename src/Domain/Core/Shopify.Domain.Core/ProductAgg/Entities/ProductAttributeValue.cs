using Shopify.Domain.Core._common;

namespace Shopify.Domain.Core.ProductAgg.Entities;

public class ProductAttributeValue : BaseEntity
{
    public int Id { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int ProductAttributeId { get; set; }
    public ProductAttribute ProductAttribute { get; set; }

    public string Value { get; set; } 
}