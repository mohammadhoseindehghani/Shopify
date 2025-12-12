using Microsoft.Extensions.Logging;
using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Service;
using Shopify.Framework;

namespace Shopify.Domain.AppService;

public class UserAppService(IUserService userService, ILogger<UserAppService> logger) : IUserAppService
{
    public async Task<Result<UserDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await userService.GetById(id,cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("با این نام کاربری کاربری یافت نشد");
        }

        return Result<UserDto>.Success(user);

    }

    public async Task<Result<UserDto>> GetByPhone(string phone, CancellationToken cansCancellationToken)
    {
        var user = await userService.GetByPhone(phone,cansCancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("با این شماره تلفن کاربری یافت نشد");
        }
        return Result<UserDto>.Success(user);
    }

    public Task<ICollection<UserDto>> GetAll(CancellationToken cancellationToken)
    {
        return userService.GetAll(cancellationToken);
    }

    public async Task<Result<bool>> Active(int userId, CancellationToken cancellationToken)
    {
        var result = await userService.Active(userId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("فعالسازی کاربر با شکست مواجه شد");
        }
        return Result<bool>.Success(result, "فعالسازی کاربر با موفقعیت انجام شد");

    }

    public async Task<Result<bool>> DeActive(int userId, CancellationToken cancellationToken)
    {
        var result = await userService.DeActive(userId, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("غیرفعالسازی کاربر با شکست مواجه شد");
        }
        return Result<bool>.Success(result, "غیرفعالسازی کاربر با موفقعیت انجام شد");
    }

    public async Task<Result<bool>> Add(CreateUserDto userDto, CancellationToken cancellationToken)
    {
        //add validation 
        var result = await userService.Add(userDto, cancellationToken);
        if (!result)
        {
            logger.LogError($"افزودن کاربر جدید با خطا مواجه شد ورودی: {userDto}");
            return Result<bool>.Failure("خطا در عملیات");
        }
        logger.LogInformation($"کاربر جدید با موفقیت ایجاد شد. نام کاربری: {userDto.Phone}");
        return Result<bool>.Success(result, "کاربر با موفقیت اینجاد شد");
    }

    public async Task<Result<bool>> ChargeWallet(int userId, decimal amount, CancellationToken cancellationToken)
    {
        if (amount <=0)
        {
            return Result<bool>.Failure("مقدار ورودی برای شارژ نامعتبر است");
        }
        var result = await userService.ChargeWallet(userId, amount, cancellationToken);
        if (!result)
        {
            return Result<bool>.Failure("خطا در عملیات");
        }
        logger.LogInformation($"کیف پول کاربر {userId} به مبلغ {amount} با موفقیت شارژ شد.");
        return Result<bool>.Success(result, "حساب کاربر با موفقیت با موفقیت شارژ شد");
    }

    public async Task<Result<UserDto>> Login(string phone, string password, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(phone))
        {
            return Result<UserDto>.Failure("شماره همراه نمیتواند خالی باشد");
        }
        if (phone.Length != 11)
        {
            return Result<UserDto>.Failure("شماره همراه باید 11 کاراکتر باشد");
        }
        if (string.IsNullOrWhiteSpace(password))
        {
            return Result<UserDto>.Failure("رمز عبود نمیتواند خالی باشد");
        }



        var user =await userService.GetByPhoneForLogin(phone, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("شماره همراه یا پسورد اشتباه می باشد");
        }

        var verifyPassword = PasswordHasherSha256.VerifyPassword(password, user.PasswordHash);
        if (!verifyPassword)
        {
            return Result<UserDto>.Failure("شماره همراه یا پسورد اشتباه می باشد");
        }
        logger.LogInformation($"ورود موفقیت آمیز کاربر با شماره {phone}");
        return Result<UserDto>.Success(user);
    }

    public async Task<Result<bool>> Register(RegisterUserDto userDto, CancellationToken cancellationToken)
    {
        //validation
        var result = await userService.Register(userDto, cancellationToken);
        if (!result)
        {
           return Result<bool>.Failure("خطا در عملیات ثبت نام");
        }
        return Result<bool>.Success(result,"ثبت نام با موفقیت انجام شد");
    }

    public async Task<int> UserCount(CancellationToken cancellationToken)
    {
        return await userService.UserCount(cancellationToken);
    }

    public async Task<Result<UserDetailDto>> GetUserDetail(int id, CancellationToken cancellationToken)
    {
        var user = await userService.GetUserDetail(id, cancellationToken);
        if (user is null)
        {
            return Result<UserDetailDto>.Failure("برای این کاربر جزئیاتی یافت نشد");
        }

        return Result<UserDetailDto>.Success(user);
    }
}