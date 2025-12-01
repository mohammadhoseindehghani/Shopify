using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.Service;

public class AddressService(IAddressRepository addressRepository) : IAddressService
{
    public async Task<AddressDto?> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        return await addressRepository.GetByUserId(userId, cancellationToken);
    }
}