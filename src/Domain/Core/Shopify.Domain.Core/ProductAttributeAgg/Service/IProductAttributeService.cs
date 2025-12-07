using Shopify.Domain.Core.ProductAttributeAgg.Dto;

namespace Shopify.Domain.Core.ProductAttributeAgg.Service;

public interface IProductAttributeService
{
    Task<bool> Add(string name, CancellationToken cancellationToken);
    Task<bool> Delete(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductAttributeDto>> GetAll(CancellationToken cancellationToken);
    Task<bool> Update(int id, string name, CancellationToken cancellationToken);
    Task<bool> ExistsByName(string name, CancellationToken cancellationToken);
}