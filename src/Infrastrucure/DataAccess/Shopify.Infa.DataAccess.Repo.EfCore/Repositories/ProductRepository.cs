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
    public async Task<bool> Add(CreateProductDto createProductDto, CancellationToken cancellationToken)
    {
        var product = new Product()
        {
            Title = createProductDto.Title,
            CategoryId = createProductDto.CategoryId,
            IsActive = true,
            IsSpecial = createProductDto.IsSpecial,
            ImageUrl = createProductDto.ImageUrl,
            Price = createProductDto.Price,
            ShortDescription = createProductDto.ShortDescription,
            StockQuantity = createProductDto.StockQuantity
        };
        context.Add(product);
        return await context.SaveChangesAsync(cancellationToken) > 0;
    }

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

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Products.Where(p => p.Id == id)
            .ExecuteDeleteAsync(cancellationToken);

        return effectedRows > 0;
    }

    public async Task<int> ProductCount(CancellationToken cancellationToken)
    {
        return await context.Products.CountAsync(cancellationToken);
    }

    public async Task<int> GetProductsInStock(CancellationToken cancellationToken)
    {
        return await context.Products.Where(p=>p.StockQuantity > 5).CountAsync(cancellationToken);
    }

    public async Task<int> GetProductsRunningLow(CancellationToken cancellationToken)
    {
        return await context.Products.Where(p => p.StockQuantity <= 5 && p.StockQuantity>0).CountAsync(cancellationToken);
    }

    public async Task<int> GetProductsOutOfStock(CancellationToken cancellationToken)
    {
        return await context.Products.Where(p => p.StockQuantity == 0).CountAsync(cancellationToken);
    }

    public async Task<bool> EditProduct(int id, EditProductDto editDto, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Products.Where(p => p.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.Title, editDto.Title)
                .SetProperty(p => p.StockQuantity, editDto.StockQuantity)
                .SetProperty(p => p.ShortDescription, editDto.ShortDescription)
                .SetProperty(p => p.IsSpecial, editDto.IsSpecial)
                .SetProperty(p => p.Price, editDto.Price)
                .SetProperty(p => p.CategoryId, editDto.CategoryId)
                .SetProperty(p => p.IsActive, editDto.IsActive)
                , cancellationToken);
        return effectedRows > 0;
    }

    public async Task<bool> EditImg(int id, string imgUrl, CancellationToken cancellationToken)
    {
        var effectedRows = await context.Products.Where(p => p.Id == id)
            .ExecuteUpdateAsync(setter => setter
                .SetProperty(p => p.ImageUrl, imgUrl), cancellationToken);
        return effectedRows > 0;
    }

    public async Task<string?> GetImg(int id, CancellationToken cancellationToken)
    {
        return await context.Products.Where(p => p.Id == id).Select(p => p.ImageUrl)
            .FirstOrDefaultAsync(cancellationToken);
    }
}