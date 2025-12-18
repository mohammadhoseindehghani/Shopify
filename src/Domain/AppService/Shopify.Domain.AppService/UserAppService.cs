using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Shopify.Domain.Core._common;
using Shopify.Domain.Core.UserAgg.AppService;
using Shopify.Domain.Core.UserAgg.Dto;
using Shopify.Domain.Core.UserAgg.Entities;
using Shopify.Domain.Core.UserAgg.Service;
using Shopify.Framework;
using System.Data;

namespace Shopify.Domain.AppService;

public class UserAppService(IUserService userService,
    ILogger<UserAppService> logger,
    SignInManager<User> signInManager,
    UserManager<User> userManager,
    RoleManager<IdentityRole<int>> roleManager) : IUserAppService
{
    public async Task<Result<UserDto>> GetById(int id, CancellationToken cancellationToken)
    {
        var user = await userService.GetById(id, cancellationToken);
        if (user is null)
        {
            return Result<UserDto>.Failure("با این نام کاربری کاربری یافت نشد");
        }
        return Result<UserDto>.Success(user);
    }

    public async Task<Result<UserDto>> GetByPhone(string phone, CancellationToken cansCancellationToken)
    {
        var user = await userService.GetByPhone(phone, cansCancellationToken);
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
        if (amount <= 0)
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



        var result = await signInManager.PasswordSignInAsync(phone, password, false, false);

        if (!result.Succeeded)
        {
            return Result<UserDto>.Failure("شماره همراه یا پسورد اشتباه می باشد");
        }
        var user = await userService.GetByPhoneForLogin(phone, cancellationToken);
        logger.LogInformation($"ورود موفقیت آمیز کاربر با شماره {phone}");
        return Result<UserDto>.Success(user,"شماره همراه یا پسورد اشتباه می باشد");

    }

    public async Task<Result<bool>> Register(RegisterUserDto userDto, CancellationToken cancellationToken)
    {
        var user = new User()
        {
            FirstName = userDto.FirstName,
            LastName = userDto.LastName,
            PhoneNumber = userDto.Phone,
            UserName = userDto.Phone,
            Email = "test@email.com"
        };

        var result = await userManager.CreateAsync(user, userDto.Password);

        if (result.Succeeded)
        {
            return Result<bool>.Success(result.Succeeded, "ثبت نام با موفقیت انجام شد");
        }
        else
        {
            var errorMessage = string.Join(", ", result.Errors.Select(e => e.Description));

            return Result<bool>.Failure(errorMessage);
        }
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
    public async Task<Result<bool>> AdminChangePassword(AdminChangePasswordDto dto, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(dto.UserId.ToString());
        if (user == null)
            return Result<bool>.Failure("کاربر یافت نشد");

        var token = await userManager.GeneratePasswordResetTokenAsync(user);

        var result = await userManager.ResetPasswordAsync(user, token, dto.NewPassword);

        if (result.Succeeded)
        {
            await userManager.UpdateSecurityStampAsync(user);

            logger.LogWarning($"ادمین رمز عبور کاربر {dto.UserId} را تغییر داد");
            return Result<bool>.Success(true, "رمز عبور کاربر با موفقیت تغییر یافت");
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Result<bool>.Failure(errors);
    }
    public async Task<Result<bool>> ChangePassword(int userId, ChangePasswordDto dto, CancellationToken cancellationToken)
    {
        if (dto.NewPassword != dto.ConfirmNewPassword)
            return Result<bool>.Failure("رمز جدید و تکرار آن مطابقت ندارند");

        var user = await userManager.FindByIdAsync(userId.ToString());
        if (user == null)
            return Result<bool>.Failure("کاربر یافت نشد");

        var result = await userManager.ChangePasswordAsync(user, dto.CurrentPassword, dto.NewPassword);

        if (result.Succeeded)
        {
            await signInManager.RefreshSignInAsync(user);

            logger.LogInformation($"کاربر {userId} با موفقیت رمز عبور خود را تغییر داد");
            return Result<bool>.Success(true, "رمز عبور با موفقیت تغییر یافت");
        }

        var errors = string.Join(", ", result.Errors.Select(e => e.Description));
        return Result<bool>.Failure(errors);
    }

    public async Task<Result<bool>> Edit(int id, EditUserDto editDto, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByIdAsync(id.ToString());

        if (user == null)
        {
            return Result<bool>.Failure("کاربر مورد نظر یافت نشد.");
        }

        user.FirstName = editDto.FirstName;
        user.LastName = editDto.LastName;
        user.ImgUrl = editDto.ImgUrl;

        user.PhoneNumber = editDto.PhoneNumber;

        if (user.Email != editDto.Email)
        {
            var existingUser = await userManager.FindByEmailAsync(editDto.Email);
            if (existingUser != null && existingUser.Id != id)
            {
                return Result<bool>.Failure("این ایمیل قبلاً توسط کاربر دیگری ثبت شده است.");
            }

            user.Email = editDto.Email;
            user.UserName = editDto.Email; 

            user.EmailConfirmed = false;
        }

        var result = await userManager.UpdateAsync(user);

        if (!result.Succeeded)
        {
            var errors = string.Join(", ", result.Errors.Select(e => e.Description));
            return Result<bool>.Failure($"ویرایش انجام نشد: {errors}");
        }

        return Result<bool>.Success(true);
    }
}