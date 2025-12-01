using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.Data;

public interface IAddressRepository
{
    Task<AddressDto?> GetByUserId(int userId, CancellationToken cancellationToken);
}