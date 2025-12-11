using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CategoryAgg.Dto;

namespace Shopify.Domain.Core.CategoryAgg.AppService;

public interface ICategoryAppService
{
    Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken);
    Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken);
    Task<Result<bool>> Add(CreateCategoryDto dto, CancellationToken cancellationToken);
    Task<Result<bool>> Edit(int id, string name, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
}