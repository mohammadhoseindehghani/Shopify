namespace Shopify.Domain.Core.ProductAgg.Dto;

public class ProductListDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsSpecial { get; set; }
}