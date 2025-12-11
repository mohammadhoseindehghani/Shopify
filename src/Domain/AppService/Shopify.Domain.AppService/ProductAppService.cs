using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.AppService;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Service;

namespace Shopify.Domain.AppService;

public class ProductAppService(IProductService productService) : IProductAppService
{
    public async Task<Result<ProductDetailDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var product =  await productService.GetById(id, cancellationToken);
        if (product is null)
            return Result<ProductDetailDto>.Failure("محصول یافت نشد");
        return Result<ProductDetailDto>.Success(product);
    }

    public async Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken)
    {
        return await productService.GetAll(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken)
    {
        return await productService.GetActiveProducts(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken)
    {
        return await productService.GetSpecialProducts(cancellationToken);
    }

    public async Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken)
    {
        return await productService.GetProductsForAdmin(cancellationToken);
    }

    public async Task<Result<bool>> ChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken)
    {
        var result =await productService.ChangeCategory(productId, newCategoryId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("خطا در عملیات");
        }

        return Result<bool>.Success(result);
    }

    public async Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken)
    {
        return await productService.GetProductsByCategory(categoryId, cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken)
    {
        return await productService.SearchProducts(keyword, cancellationToken);
    }

    public async Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken)
    {
        return await productService.GetProductsWithAttributes(cancellationToken);
    }

    public async Task<Result<bool>> Delete(int id, CancellationToken cancellationToken)
    {
        var result = await productService.Delete(id, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("عملیات حذف با شکست مواجه شد");
        }

        return Result<bool>.Success(result, "حذف با موفقیت انجام شد");
    }

    public async Task<int> ProductCount(CancellationToken cancellationToken)
    {
        return await productService.ProductCount(cancellationToken);
    }

    public async Task<int> GetProductsInStock(CancellationToken cancellationToken)
    {
        return await productService.GetProductsInStock(cancellationToken);
    }

    public async Task<int> GetProductsRunningLow(CancellationToken cancellationToken)
    {
        return await productService.GetProductsRunningLow(cancellationToken);
    }

    public async Task<int> GetProductsOutOfStock(CancellationToken cancellationToken)
    {
        return await productService.GetProductsOutOfStock(cancellationToken);
    }
}