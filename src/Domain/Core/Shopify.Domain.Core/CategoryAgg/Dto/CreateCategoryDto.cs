namespace Shopify.Domain.Core.CategoryAgg.Dto;

public class CreateCategoryDto
{
    public string Name { get; set; }
    public int? ParentId { get; set; }
}