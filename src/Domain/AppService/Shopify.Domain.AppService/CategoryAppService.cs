using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Domain.Core.CategoryAgg.Service;

namespace Shopify.Domain.AppService;

public class CategoryAppService(ICategoryService categoryService) : ICategoryAppService
{
    public async Task<Result<CategoryDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var category =  await categoryService.GetById(id, cancellationToken);
        if (category == null)
            return Result<CategoryDto>.Failure("دسته بندی یافت نشد");
        return Result<CategoryDto>.Success(category);
    }

    public async Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await categoryService.GetAll(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken)
    {
        return await categoryService.GetAllParents(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken)
    {
        return await categoryService.GetSubCategories(parentId, cancellationToken);
    }

    public async Task<Result<bool>> ExistsByName(string name, CancellationToken cancellationToken)
    {
        var exists = await categoryService.ExistsByName(name, cancellationToken);
        return Result<bool>.Success(exists);
    }
}