using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.AppService;

public class AddressAppService(IAddressService addressService) : IAddressAppService
{
    public async Task<Result<AddressDto>> GetByUserId(int userId, CancellationToken cancellationToken)
    {
        var address = await addressService.GetByUserId(userId, cancellationToken);
        if (address is null)
            return Result<AddressDto>.Failure("آدرس پیدا نشد");
        return Result<AddressDto>.Success(address);
    }
}