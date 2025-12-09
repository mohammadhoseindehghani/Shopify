using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Shopify.Domain.Core.ProductAgg.Data;
using Shopify.Domain.Core.ProductAgg.Dto;
using Shopify.Domain.Core.ProductAgg.Entities;
using Shopify.Domain.Core.ProductAttributeAgg.Entities;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task<ProductDetailDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(p => p.Id == id)
            .Select(p => new ProductDetailDto
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsActive = p.IsActive,
                IsSpecial = p.IsSpecial,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                AttributeValues = p.AttributeValues.Select(av => new ProductAttributeValueDto
                {
                    ProductAttributeId = av.ProductAttributeId,
                    AttributeName = av.ProductAttribute.Name,
                    Value = av.Value
                }).ToList()
            }).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Products
            .Select(p => new ProductListDto()
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsSpecial = p.IsSpecial
            }).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<AdminProductDto>> GetProductsForAdmin(CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .Select(p => new AdminProductDto()
        {
            Id = p.Id,
            Title = p.Title,
            ImageUrl = p.ImageUrl,
            Price = p.Price,
            IsSpecial = p.IsSpecial,
            IsActive = p.IsActive,
            CategoryId = p.CategoryId,
            StockQuantity = p.StockQuantity,
            CategoryName = p.Category.Name,
        }).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetActiveProducts(CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(p => p.IsActive)
            .Select(p => new ProductListDto()
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsSpecial = p.IsSpecial
            }).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> GetSpecialProducts(CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(p => p.IsSpecial)
            .Select(p => new ProductListDto()
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsSpecial = p.IsSpecial
            }).ToListAsync(cancellationToken);
    }

    public async Task<bool> ChangeCategory(int productId, int newCategoryId, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Products
            .Where(p => p.Id == productId)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.CategoryId, newCategoryId), cancellationToken);
        return effectedRows > 0;
    }

    public async Task<ICollection<ProductListDto>> GetProductsByCategory(int categoryId, CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(p => p.CategoryId == categoryId)
            .Select(p => new ProductListDto()
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsSpecial = p.IsSpecial
            }).ToListAsync(cancellationToken);
    }

    public async Task<ICollection<ProductListDto>> SearchProducts(string keyword, CancellationToken cancellationToken)
    {
        return await context.Products
            .Where(p => p.Title.Contains(keyword))
            .Select(p => new ProductListDto()
            {
                Id = p.Id,
                Title = p.Title,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsSpecial = p.IsSpecial
            }).ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByTitle(string title, CancellationToken cancellationToken)
    {
        return await context.Products
            .AnyAsync(p => p.Title == title, cancellationToken);
    }

    public async Task<ICollection<ProductDetailDto>> GetProductsWithAttributes(CancellationToken cancellationToken)
    {
        return await context.Products
            .Select(p => new ProductDetailDto
            {
                Id = p.Id,
                Title = p.Title,
                ShortDescription = p.ShortDescription,
                ImageUrl = p.ImageUrl,
                Price = p.Price,
                IsActive = p.IsActive,
                IsSpecial = p.IsSpecial,
                StockQuantity = p.StockQuantity,
                CategoryId = p.CategoryId,
                CategoryName = p.Category.Name,
                AttributeValues = p.AttributeValues
                    .Select(av => new ProductAttributeValueDto
                    {
                        ProductAttributeId = av.ProductAttributeId,
                        AttributeName = av.ProductAttribute.Name,
                        Value = av.Value
                    }).ToList()
            })
            .ToListAsync(cancellationToken);
    }

    public async Task ReduceStock(int productId, int quantity, CancellationToken cancellationToken)
    {
        await context.Products
            .Where(p => p.Id == productId)
            .ExecuteUpdateAsync(
                setter => setter
                    .SetProperty(p => p.StockQuantity, p => p.StockQuantity - quantity),
                cancellationToken
            );
    }
}