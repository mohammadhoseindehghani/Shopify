using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.AppService;

public interface IAddressAppService
{
    Task<Result<AddressDto>> GetByUserId(int userId , CancellationToken cancellationToken);
}