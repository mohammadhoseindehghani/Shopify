using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Entities;
using System.Threading;

namespace Shopify.Domain.Core.UserAgg.AppService;

public interface IUserAppService
{
    Task<Result<UserDto>> GetById(int id, CancellationToken cancellationToken);
    Task<Result<UserDto>> GetByPhone(string phone, CancellationToken cansCancellationToken);
    Task<ICollection<UserDto>> GetAll(CancellationToken cancellationToken);
    Task<Result<bool>> Active(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> DeActive(int userId, CancellationToken cancellationToken);
    Task<Result<bool>> Add(CreateUserDto userDto, CancellationToken cancellationToken);
    Task<Result<bool>> ChargeWallet(int userId, decimal amount, CancellationToken cancellationToken);
    Task<Result<UserDto>> Login(string phone,string password, CancellationToken cancellationToken);
}