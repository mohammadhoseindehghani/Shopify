using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shopify.Domain.Core.CategoryAgg.Entities;
public class Category : BaseEntity
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
    public Category Parent { get; set; }
    public ICollection<Category> SubCategories { get; set; }
    public ICollection<Product> Products { get; set; }
}