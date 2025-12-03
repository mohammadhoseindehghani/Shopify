namespace Shopify.Domain.Core.CartAgg.Dto;

public class CartDto
{
    public int Id { get; set; }
    public List<CartItemDto> Items { get; set; } = [];

}