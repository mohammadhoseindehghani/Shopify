using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.Service;

public interface IUserService
{
    Task<UserDto?> GetById(int id, CancellationToken cancellationToken);
    Task<UserDto?> GetByPhone(string phone, CancellationToken cansCancellationToken);
    Task<UserWithPasswordDto?> GetByPhoneForLogin(string phone, CancellationToken cancellationToken);
}