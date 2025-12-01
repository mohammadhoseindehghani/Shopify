using Shopify.Domain.Core.UserAgg.Data;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Service;

namespace Shopify.Domain.Service;

public class UserService(IUserRepository userRepository) : IUserService
{
    public async Task<UserDto?> GetById(int id, CancellationToken cancellationToken)
    {
        return await userRepository.GetById(id, cancellationToken);
    }

    public async Task<UserDto?> GetByPhone(string phone, CancellationToken cansCancellationToken)
    {
        return await userRepository.GetByPhone(phone, cansCancellationToken);
    }

    public async Task<UserWithPasswordDto?> GetByPhoneForLogin(string phone, CancellationToken cancellationToken)
    {
        return await userRepository.GetByUserNameForLogin(phone, cancellationToken);
    }
}