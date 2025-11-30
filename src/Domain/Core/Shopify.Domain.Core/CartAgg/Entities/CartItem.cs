using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Entities;

namespace Shopify.Domain.Core.CartAgg.Entities;

public class CartItem : BaseEntity
{
    public int CartId { get; set; }
    public Cart Cart { get; set; }

    public int ProductId { get; set; }
    public Product Product { get; set; }

    public int Quantity { get; set; }

}