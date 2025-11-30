using Shopify.Domain.Core.CategoryAgg.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Shopify.Domain.Core._common;

namespace Shopify.Domain.Core.ProductAgg.Entities;

public class Product : BaseEntity
{
public string Title { get; set; } 
public string ShortDescription { get; set; } 
public string ImagePath { get; set; } 
public decimal Price { get; set; }
public bool IsActive { get; set; } = true;
public bool IsSpecial { get; set; } 
public int StockQuantity { get; set; } 
public int CategoryId { get; set; }
public Category Category { get; set; }
public ICollection<ProductAttributeValue> AttributeValues { get; set; }
}