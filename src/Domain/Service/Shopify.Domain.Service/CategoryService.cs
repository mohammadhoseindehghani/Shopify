using Shopify.Domain.Core.CategoryAgg.Data;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Domain.Core.CategoryAgg.Service;

namespace Shopify.Domain.Service;

public class CategoryService(ICategoryRepository categoryRepository) : ICategoryService
{
    public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetById(id, cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAll(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken)
    {
        return await categoryRepository.GetAllParents(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken)
    {
        return await categoryRepository.GetSubCategories(parentId, cancellationToken);
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await categoryRepository.ExistsByName(name, cancellationToken);
    }

    public async Task<bool> Add(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        if (dto.ParentId.HasValue)
        {
            var parentExists = await categoryRepository.Exists(dto.ParentId.Value, cancellationToken);

            if (!parentExists)
            {
                throw new Exception("دسته بندی پدر وجود ندارد");
            }
        }
        return await categoryRepository.Add(dto, cancellationToken);
    }

    public async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await categoryRepository.Exists(id, cancellationToken);
    }
}