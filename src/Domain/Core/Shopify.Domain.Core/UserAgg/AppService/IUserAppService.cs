using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.AppService;

public interface IUserAppService
{
    Task<Result<UserDto>> GetById(int id, CancellationToken cancellationToken);
    Task<Result<UserDto>> GetByPhone(string phone, CancellationToken cansCancellationToken);
    Task<Result<UserDto>> Login(string Phone,string password, CancellationToken cancellationToken);
}