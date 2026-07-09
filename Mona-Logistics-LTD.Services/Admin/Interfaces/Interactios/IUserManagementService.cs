using Mona_Logistics_LTD.Web.ViewModels.Admin.Interactions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.Admin.Interfaces.Interactios;


public interface IUserManagementService
{
    Task<IEnumerable<UserManagmentIndexViewModel>> GetUserManagmentBoardDataAsync(Guid userId);

    Task<UserManagmentIndexViewModel> FindUserByIdAsync(string userId);

    Task<(bool Failed, string ErrorMessage)> DisableUser(string userId);

    Task<(bool Failed, string ErrorMessage)> ChangeUserRoleAsync(
        ChangeUserRoleViewModel model,
        Guid adminId);
}