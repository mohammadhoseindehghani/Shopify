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

    public async Task<bool> ChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken)
    {
        return await productRepository.ChangeCategory(productId, newCategoryId, cancellationToken);
    }

    public async Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsForAdmin(cancellationToken);
    }

    public async Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsWithAttributes(cancellationToken);
    }

    public async Task ReduceStock(int productId, int quantity, CancellationToken cancellationToken)
    {
        await productRepository.ReduceStock(productId, quantity, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await productRepository.Delete(id, cancellationToken);
    }

    public async Task<int> ProductCount(CancellationToken cancellationToken)
    {
        return await productRepository.ProductCount(cancellationToken);
    }

    public async Task<int> GetProductsInStock(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsInStock(cancellationToken);
    }

    public async Task<int> GetProductsRunningLow(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsRunningLow(cancellationToken);
    }

    public async Task<int> GetProductsOutOfStock(CancellationToken cancellationToken)
    {
        return await productRepository.GetProductsOutOfStock(cancellationToken);
    }
}