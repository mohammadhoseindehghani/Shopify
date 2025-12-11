using Shopify.Domain.Core._common;
using Shopify.Domain.Core.CategoryAgg.AppService;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Domain.Core.CategoryAgg.Service;
using System.Xml.Linq;

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

    public async Task<Result<bool>> Add(CreateCategoryDto dto, CancellationToken cancellationToken)
    {
        try
        {
            if (dto.Name == null!)
            {
                return Result<bool>.Failure("اطلاعات ورودی نمی‌تواند خالی باشد.");
            }

            if (string.IsNullOrWhiteSpace(dto.Name))
            {
                return Result<bool>.Failure("نام دسته‌بندی الزامی است.");
            }

            await categoryService.Add(dto, cancellationToken);


            return Result<bool>.Success(true, "دسته‌بندی با موفقیت ایجاد شد.");
        }
        catch (Exception ex)
        {

            return Result<bool>.Failure(ex.Message);
        }
    }

    public async Task<Result<bool>> Edit(int id, string name, CancellationToken cancellationToken)
    {
        var result = await categoryService.Edit(id, name, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("ادیت نشد");
        }
        return Result<bool>.Success(result,"ادیت انجام شد");
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await categoryService.Delete(id, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("حذف نشد");
        }
        return Result<bool>.Success(result, "حذف انجام شد");
    }

    public async Task<Result<bool>> ExistsByName(string name, CancellationToken cancellationToken)
    {
        var exists = await categoryService.ExistsByName(name, cancellationToken);
        return Result<bool>.Success(exists);
    }
}