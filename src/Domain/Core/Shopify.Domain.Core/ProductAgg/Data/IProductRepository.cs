using Shopify.Domain.Core.ProductAgg.Dto;

namespace Shopify.Domain.Core.ProductAgg.Data;

public interface IProductRepository
{
    Task<ProductDetailDto?> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken);
    Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken);
    Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken);
    Task ReduceStock(int productId, int quantity, CancellationToken cancellationToken);
}
