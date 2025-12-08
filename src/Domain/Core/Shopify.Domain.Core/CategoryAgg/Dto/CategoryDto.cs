namespace Shopify.Domain.Core.CategoryAgg.Dto;

public class CategoryDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int? ParentId { get; set; }

}