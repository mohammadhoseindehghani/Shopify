using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Domain.Core.OrderAgg.Entities;

public class OrderItem :BaseEntity
{
    public int OrderId { get; set; }
    public Order Order { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; } 
    public decimal UnitPrice { get; set; } 
}