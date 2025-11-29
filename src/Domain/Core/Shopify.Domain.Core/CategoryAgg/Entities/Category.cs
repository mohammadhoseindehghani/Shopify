using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Domain.Core.CategoryAgg.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; } 
    public ICollection<Product> Products { get; set; }
}