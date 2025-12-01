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
}