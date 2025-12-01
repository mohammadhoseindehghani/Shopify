using Shopify.Domain.Core.ProductAgg.Data;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Service;

namespace Shopify.Domain.Service;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ProductDetailDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await productRepository.GetById(id, cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken)
    {
        return await productRepository.GetAll(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken)
    {
        return await productRepository.GetActiveProducts(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken)
    {
        return await productRepository.GetSpecialProducts(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsByCategory(categoryId, cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken)
    {
        return await productRepository.SearchProducts(keyword, cancellationToken);
    }

    public async Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken)
    {
        return await productRepository.ExistsByTitle(title, cancellationToken);
    }

    public async Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsWithAttributes(cancellationToken);
    }
}