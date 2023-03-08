// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.Threading.Tasks;
using Service.Model;
using Common;
namespace Service;
public interface IAccountService
{
    Task ChangePassword(string userId, string oldPassword, string newPassword);
    Task CreateAdminAccount();
    Task InstructorAccount(InstructorAccountDto dto);
    Task<LoginResultDto> RefreshToken(string accessToken, string refreshToken);
    Task ResetPassword(PasswordResetDto passwordReset);
    Task<string> ResetPasswordSendLink(string email);
    Task<LoginResultDto> SignAsync(LoginDto login);
    Task Revoke(string userId);
}
