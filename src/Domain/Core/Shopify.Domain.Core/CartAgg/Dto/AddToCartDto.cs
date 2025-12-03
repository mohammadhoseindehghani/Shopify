namespace Shopify.Domain.Core.CartAgg.Dto;

public class AddToCartDto
{
    public int? UserId { get; set; }
    public Guid? GuestId { get; set; }
    public int ProductId { get; set; }
    public int Quantity { get; set; }
}