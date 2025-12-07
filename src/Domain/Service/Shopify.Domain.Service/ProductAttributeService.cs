using Shopify.Domain.Core.ProductAttributeAgg.Data;
using Shopify.Domain.Core.ProductAttributeAgg.Dto;
using Shopify.Domain.Core.ProductAttributeAgg.Service;

namespace Shopify.Domain.Service;

public class ProductAttributeService(IProductAttributeRepository productAttributeRepository) : IProductAttributeService
{
    public async Task<bool> Add(string name, CancellationToken cancellationToken)
    {
        return await productAttributeRepository.Add(name, cancellationToken);
    }

    public async Task<bool> Delete(int id, CancellationToken cancellationToken)
    {
        return await productAttributeRepository.Delete(id, cancellationToken);
    }

    public async Task<ICollection<ProductAttributeDto>> GetAll(CancellationToken cancellationToken)
    {
        return await productAttributeRepository.GetAll(cancellationToken);
    }

    public async Task<bool> Update(int id, string name, CancellationToken cancellationToken)
    {
        return await productAttributeRepository.Update(id, name, cancellationToken);
    }

    public async Task<bool> ExistsByName(string name, CancellationToken cancellationToken)
    {
        return await productAttributeRepository.ExistsByName(name, cancellationToken);
    }
}