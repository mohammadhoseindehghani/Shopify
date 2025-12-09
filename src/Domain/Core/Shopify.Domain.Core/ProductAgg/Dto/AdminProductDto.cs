namespace Shopify.Domain.Core.ProductAgg.Dto;

public class AdminProductDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string ImageUrl { get; set; }
    public decimal Price { get; set; }
    public bool IsSpecial { get; set; }

    public int StockQuantity { get; set; }
    public string CategoryName { get; set; }
    public int CategoryId { get; set; } 
    public bool IsActive { get; set; }
}