namespace Shopify.Domain.Core.CartAgg.Dto;

public class CartDto
{
    public int Id { get; set; }
    public int? UserId { get; set; }
    public int? GuestId { get; set; }
    public ICollection<CartItemDto> Items { get; set; }
    public decimal TotalAmount => Items?.Sum(i => i.TotalPrice) ?? 0;
}