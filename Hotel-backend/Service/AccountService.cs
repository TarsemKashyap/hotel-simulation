using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Model;

namespace Service;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> _roleManager;
    public AccountService(UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager, IConfiguration configuration)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task CreateAdminAccount()
    {
        //todo: read admin account from App setting
        var appuser = new AppUser
        {
            UserName = "greatGursoy",
            PasswordHash = "12345"
        };
        var adminAccount = await _userManager.FindByIdAsync(appuser.UserName);
        if (adminAccount != null)
        {
            throw new ValidationException("Admin account already exists");
        }

        var result = await _userManager.CreateAsync(appuser);
        if (!await _roleManager.RoleExistsAsync(RoleType.Admin.ToString()))
        {
            await _userManager.AddToRoleAsync(appuser, RoleType.Admin.ToString());
        }

    }

    public async Task InstructorAccount(InstructorAccountDto dto)
    {
        var appuser = new Instructor
        {
            UserName = dto.Email,
            PasswordHash = dto.Password,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Institue = dto.Institue
        };
        var user = await _userManager.FindByEmailAsync(dto.Email);
        if (user != null)
        {
            throw new ValidationException("User with {0} this email already exist", dto.Email);
        }
        var result = await _userManager.CreateAsync(appuser);
        if (result.Succeeded)
        {
            await CreateRoleifNotExist(RoleType.Instructor);
            await _userManager.AddToRoleAsync(appuser, RoleType.Instructor);
        }
    }

    private async Task CreateRoleifNotExist(string role)
    {
        if (!await _roleManager.RoleExistsAsync(role))
        {
            await _roleManager.CreateAsync(new AppUserRole(role));
        }
    }



    public async Task<string> ResetPasswordSendLink(string email)
    {
        var appUser = await _userManager.FindByEmailAsync(email);
        var token = await _userManager.GeneratePasswordResetTokenAsync(appUser);
        return token;
    }

    public async Task ResetPassword(PasswordResetDto passwordReset)
    {
        var appuser = await _userManager.FindByEmailAsync(passwordReset.Email);
        if (appuser == null)
            throw new ValidationException("User doest not exist");
        var result = await _userManager.ResetPasswordAsync(appuser, passwordReset.Token, passwordReset.Password);
        if (!result.Succeeded)
        {

            throw new ValidationException("Error while resetting password");
        }
    }

    public async Task ChangePassword(string userId, string oldPassword, string newPassword)
    {
        var appUser = await _userManager.FindByIdAsync(userId);
        if (appUser == null)
            throw new ValidationException("Account does not exist for given user");
        await _userManager.ChangePasswordAsync(appUser, oldPassword, newPassword);
    }

}

public class ValidationException : System.Exception
{
    public ValidationException(string message) : base(message)
    {

    }
    public ValidationException(string message, params object[] args) : this(FormatMessage(message, args))
    {

    }
    public static string FormatMessage(string message, params object[] args) => string.Format(message, args);
}
