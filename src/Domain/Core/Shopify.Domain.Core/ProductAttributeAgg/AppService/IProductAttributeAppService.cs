using Shopify.Domain.Core._common;
using Shopify.Domain.Core.ProductAttributeAgg.Dto;

namespace Shopify.Domain.Core.ProductAttributeAgg.AppService;

public interface IProductAttributeAppService
{
    Task<Result<bool>> Add(string name, CancellationToken cancellationToken);
    Task<Result<bool>> Delete(int id, CancellationToken cancellationToken);
    Task<ICollection<ProductAttributeDto>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> Update(int id, string name, CancellationToken cancellationToken);
}