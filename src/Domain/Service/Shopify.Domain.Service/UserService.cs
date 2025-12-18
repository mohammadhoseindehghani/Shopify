using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task DeductBalance(int userId, decimal amount, CancellationToken cancellationToken)
    {
        await userRepository.DeductBalance(userId, amount, cancellationToken);
    }
    public async Task<UserDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await userRepository.GetById(id, cancellationToken);
    }

    public async Task<UserDto?> GetByPhone(string phone, CancellationToken cansCancellationToken)
    {
        return await userRepository.GetByPhone(phone, cansCancellationToken);
    }

    public Task<ICollection<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        return userRepository.GetAll(cancellationToken);
    }

    public async Task<bool> ChargeWallet(int userId, decimal amount, CancellationToken cancellationToken)
    {
        return await userRepository.ChargeWallet(userId, amount, cancellationToken);
    }

    public async Task<bool> Add(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        return await userRepository.Add(userDto, cancellationToken);
    }

    public async Task<bool> Active(int userId, CancellationToken cancellationToken)
    {
        return await userRepository.Active(userId, cancellationToken);
    }

    public async Task<bool> DeActive(int userId, CancellationToken cancellationToken)
    {
        return await userRepository.DeActive(userId, cancellationToken);
    }

    public async Task<UserWithPasswordDto?> GetByPhoneForLogin(string phone, CancellationToken cancellationToken)
    {
        return await userRepository.GetByUserNameForLogin(phone, cancellationToken);
    }

    public async Task<int> UserCount(CancellationToken cancellationToken)
    {
        return await userRepository.UserCount(cancellationToken);
    }

    public async Task<UserDetailDto?> GetUserDetail(int id, CancellationToken cancellationToken)
    {
        return await userRepository.GetUserDetail(id, cancellationToken);
    }


}