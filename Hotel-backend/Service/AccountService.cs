using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Service.Model;
using Database;
using Microsoft.Extensions.Options;

namespace Service;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppUserRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly ITokenService _tokenService;
    private readonly HotelDbContext _context;
    private readonly JwtSettings _jwtSettings;

    public AccountService(UserManager<AppUser> userManager, RoleManager<AppUserRole> roleManager, IConfiguration configuration, SignInManager<AppUser> signInManager, ITokenService tokenService, HotelDbContext context, IOptions<JwtSettings> jwtSettings)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _tokenService = tokenService;
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }

    public async Task CreateAdminAccount()
    {
        //todo: read admin account from App setting
        var appuser = new AppUser
        {
            UserName = "greatGursoy"
        };
        var adminAccount = await _userManager.FindByNameAsync(appuser.UserName);
        if (adminAccount != null)
        {
            throw new ValidationException("Admin account already exists");
        }

        var result = await _userManager.CreateAsync(appuser, "12345");
        if (!await _roleManager.RoleExistsAsync(RoleType.Admin))
        {
            await _roleManager.CreateAsync(new AppUserRole(RoleType.Admin));
        }
        var accountresult = await _userManager.AddToRoleAsync(appuser, RoleType.Admin);
        if (accountresult != null)
        {

        }

    }

    public async Task InstructorAccount(InstructorAccountDto dto)
    {
        var appuser = new Instructor
        {
            UserName = dto.Email,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Institue = dto.Institue,
            TwoFactorEnabled = false
        };
        var user = await _userManager.FindByNameAsync(dto.Email);
        if (user != null)
        {
            throw new ValidationException("User with {0} this email already exist", dto.Email);
        }
        var result = await _userManager.CreateAsync(appuser, dto.Password);
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
        var appUser = await _userManager.FindByNameAsync(userId);
        if (appUser == null)
            throw new ValidationException("Account does not exist for given user");
        var result = await _userManager.ChangePasswordAsync(appUser, oldPassword, newPassword);
        if (!result.Succeeded)
        {
            throw new ValidationException(result.Errors.Select(x => x.Description).ToArray());
        }

    }

    public async Task<LoginResultDto> SignAsync(LoginDto login)
    {
        var user = await _userManager.FindByNameAsync(login.UserId);
        if (user is null)
            throw new ValidationException("User not found");
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, login.Password, false);
        if (!signInResult.Succeeded)
            throw new ValidationException("UserId or password is incorrect");

        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.NameIdentifier, user.Id),
            new Claim(ClaimTypes.Name, user.UserName),
        };
        var accessToken = _tokenService.GenerateAccessToken(claims);
        var refreshToken = _tokenService.GenerateRefreshToken();
        _context.RefreshTokens.Add(new AppUserRefreshToken()
        {
            UserId = user.Id,
            RefreshToken = refreshToken,
            IsActive = true,
            ExpiryTime = DateTime.Now.AddMinutes(_jwtSettings.RefreshTokenExpirationMinutes),
            CreatedDate = DateTime.Now
        });
        await _context.SaveChangesAsync();

        var roles = await _userManager.GetRolesAsync(user);
        return new LoginResultDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            Roles = roles.ToArray()
        };


    }

    public async Task<LoginResultDto> RefreshToken(string accessToken, string refreshToken)
    {
        var principal = _tokenService.GetPrincipalFromExpiredToken(accessToken);
        string userName = principal.Identity.Name;
        var appuser = await _userManager.FindByNameAsync(userName);
        var storeRefreshToken = _context.RefreshTokens.FirstOrDefault(x => x.UserId.Equals(appuser.Id) && x.RefreshToken.Equals(refreshToken));
        if (appuser is null || storeRefreshToken is null || storeRefreshToken.ExpiryTime <= DateTime.Now)
        {
            throw new UnauthorizedAccessException();
        }
        var newAccessToken = _tokenService.GenerateAccessToken(principal.Claims);
        var newRefreshToken = _tokenService.GenerateRefreshToken();
        storeRefreshToken.RefreshToken = newRefreshToken;
        _context.RefreshTokens.Update(storeRefreshToken);
        _context.SaveChanges();
        return new LoginResultDto { AccessToken = newAccessToken, RefreshToken = newRefreshToken };
    }

    public async Task Revoke(string userId)
    {
        var tokens = _context.RefreshTokens.Where(x => x.UserId.Equals(userId)).AsEnumerable();
        _context.RefreshTokens.RemoveRange(tokens);
        await _context.SaveChangesAsync();
    }

}

public class ValidationException : System.Exception
{
    private readonly string[] _message;

    public ValidationException(string message) : base(message)
    {

    }
    public ValidationException(params string[] message)
    {
        _message = message;
    }
    public ValidationException(string message, params object[] args) : this(FormatMessage(message, args))
    {

    }
    public static string FormatMessage(string message, params object[] args) => string.Format(message, args);
}
