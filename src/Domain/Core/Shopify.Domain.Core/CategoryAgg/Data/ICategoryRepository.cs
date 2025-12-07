using Shopify.Domain.Core.CategoryAgg.Dto;

namespace Shopify.Domain.Core.CategoryAgg.Data;

public interface ICategoryRepository
{
    Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetSubCategories(int parentId,CancellationToken cancellationToken);
    Task<bool> ExistsByName(string name,CancellationToken cancellationToken);
    Task<bool> Add(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken);
}
