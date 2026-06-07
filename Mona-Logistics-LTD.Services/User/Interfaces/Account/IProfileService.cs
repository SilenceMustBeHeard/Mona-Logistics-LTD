using Mona_Logistics_LTD.Web.ViewModels.User.Account.Profile;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Account;

public interface IProfileService
{
    Task<ProfileViewModel?> GetProfileAsync(string userId);
}