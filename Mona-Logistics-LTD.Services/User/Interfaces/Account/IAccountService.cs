using Mona_Logistics_LTD.Web.ViewModels.User.Account.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Account;


public interface IAccountService
{

    Task<(bool Success, string[] Errors)> RegisterAsync(RegisterViewModel model);

    Task<bool> LoginAsync(LoginViewModel model);

    Task LogoutAsync();

    Task<bool> ForgotPasswordAsync(string email, string resetLink);
    Task<(bool Success, string[] Errors)> ResetPasswordAsync(ResetPasswordViewModel model);
}