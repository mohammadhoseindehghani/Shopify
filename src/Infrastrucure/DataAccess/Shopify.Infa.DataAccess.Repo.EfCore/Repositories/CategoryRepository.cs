using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.CategoryAgg.Data;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Domain.Core.CategoryAgg.Entities;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Categories.Where(c => c.Id == id)
            .AsNoTracking()
            .Select(x => new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Categories
            .AsNoTracking()
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
                ParentId = c.ParentId
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => c.ParentId == null)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => c.ParentId == parentId)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync(cancellationToken);
    }


    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await context.Categories.AnyAsync(c => c.Name == name, cancellationToken);
    }

    public async Task<bool> Add(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = createCategoryDto.Name,
            ParentId = createCategoryDto.ParentId 
        };
        context.Categories.Add(category);
        return await context.SaveChangesAsync(cancellationToken) > 0;

    }

    public async Task<bool> Exists(int id, CancellationToken cancellationToken)
    {
        return await context.Categories.AnyAsync(c => c.Id == id, cancellationToken);
    }
}