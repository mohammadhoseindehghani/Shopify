using Shopify.Domain.Core.UserAgg.Dto;

namespace Shopify.Domain.Core.UserAgg.Service;

public interface IUserService
{
    Task DeductBalance(int userId, decimal amount, CancellationToken cancellationToken);
    Task<UserDto?> GetById(int id, CancellationToken cancellationToken);
    Task<UserDto?> GetByPhone(string phone, CancellationToken cansCancellationToken);
    Task<ICollection<UserDto>> GetAll(CancellationToken cancellationToken);
    Task<bool> ChargeWallet(int userId, decimal amount, CancellationToken cancellationToken);
    Task<bool> Add(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<bool> Active(int userId, CancellationToken cancellationToken);
    Task<bool> DeActive(int userId, CancellationToken cancellationToken);
    Task<UserWithPasswordDto?> GetByPhoneForLogin(string phone, CancellationToken cancellationToken);
    Task<int> UserCount(CancellationToken cancellationToken);
    Task<UserDetailDto?> GetUserDetail(int id, CancellationToken cancellationToken);
}