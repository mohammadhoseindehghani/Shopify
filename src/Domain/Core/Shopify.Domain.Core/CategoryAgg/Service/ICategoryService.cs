using Shopify.Domain.Core.CategoryAgg.Dto;

namespace Shopify.Domain.Core.CategoryAgg.Service;

public interface ICategoryService
{
    Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken);
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
}