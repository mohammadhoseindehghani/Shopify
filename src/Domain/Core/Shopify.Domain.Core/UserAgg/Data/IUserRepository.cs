using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.Data;

public interface IUserRepository
{
    Task<UserDto?> GetById(int id,CancellationToken cancellationToken);
    Task<UserDto?> GetByPhone(string phone , CancellationToken cansCancellationToken);
    Task<UserWithPasswordDto?> GetByUserNameForLogin(string username, CancellationToken cancellationToken);
}