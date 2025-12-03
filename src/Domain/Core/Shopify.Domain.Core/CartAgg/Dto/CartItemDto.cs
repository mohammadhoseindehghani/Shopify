namespace Shopify.Domain.Core.CartAgg.Dto;

public class CartItemDto
{
    public int ProductId { get; set; }
    public int Quantity { get; set; }
    public string ProductTitle { get; set; }
    public string ImageUrl { get; set; }
    public decimal UnitPrice { get; set; }
}
