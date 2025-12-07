using Microsoft.EntityFrameworkCore;
using Shopify.Domain.Core.CategoryAgg.Data;
using Shopify.Domain.Core.CategoryAgg.Dto;
using Shopify.Infa.Db.SqlServer.EfCore.DbContexts;

namespace Shopify.Infa.DataAccess.Repo.EfCore.Repositories;

public class CategoryRepository(AppDbContext context) : ICategoryRepository
{
    public async Task<CategoryDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await context.Categories.Where(c => c.Id == id)
            .Select(x=>new CategoryDto()
            {
                Id = x.Id,
                Name = x.Name,
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAll(CancellationToken cancellationToken)
    {
        return await context.Categories
            .Select(c => new CategoryDto()
            {
                Id = c.Id,
                Name = c.Name,
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetAllParents(CancellationToken cancellationToken)
    {
        return await context.Categories
            .Where(c => c.ParentId == null)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(cancellationToken);
    }

    public async Task<ICollection<CategoryDto>> GetSubCategories(int parentId, CancellationToken cancellationToken)
    {
        return await context.Categories
            .Where(c => c.ParentId == parentId)
            .Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name
            })
            .ToListAsync(cancellationToken);
    }


    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await context.Categories.AnyAsync(c => c.Name == name,cancellationToken);
    }

    public Task<bool> Add(CreateCategoryDto createCategoryDto, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}