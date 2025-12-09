using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Entities;
using Shopify.Domain.Core.ProductAttributeAgg.Entities;
using System.Threading;

namespace Shopify.Domain.Core.ProductAgg.Data;

public interface IProductRepository
{
    Task<ProductDetailDto?> GetById(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken);
    Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken); 
    Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken);
    Task<bool> ChangeCategory(int productId,int newCategoryId,CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken);
    Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken);
    Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken);
    Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken);
    Task ReduceStock(int productId, int quantity, CancellationToken cancellationToken);

}
