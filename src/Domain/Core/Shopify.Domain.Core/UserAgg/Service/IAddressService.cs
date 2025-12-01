using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.Service;

public interface IAddressService
{
    Task<AddressDto?> GetByUserId(int userId , CancellationToken cancellationToken);
}