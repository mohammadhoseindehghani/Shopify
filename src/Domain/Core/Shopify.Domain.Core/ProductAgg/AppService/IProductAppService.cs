using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAgg.Dto;

namespace Shopify.Domain.Core.ProductAgg.AppService;

public interface IProductAppService
{
    Task<Result<ProductDetailDto>> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken);
    Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken);
    Task<Result<bool>> ChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken);

    Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken);
    Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken);

}