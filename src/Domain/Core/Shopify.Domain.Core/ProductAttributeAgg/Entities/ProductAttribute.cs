using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Domain.Core.ProductAttributeAgg.Entities;

public class ProductAttribute : BaseEntity
{
    public string Name { get; set; }
    public ICollection<ProductAttributeValue> AttributeValues { get; set; }

}