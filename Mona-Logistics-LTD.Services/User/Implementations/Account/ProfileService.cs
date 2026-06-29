using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Account;
using Mona_Logistics_LTD.Services.Admin.Interfaces.Message;
using Mona_Logistics_LTD.Services.User.Interfaces.Account;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account.Profile;

namespace Mona_Logistics_LTD.Services.User.Implementations.Account;

public class ProfileService : IProfileService
{
    private readonly IAppUserRepository _userRepository;
    private readonly UserManager<AppUser> _userManager;
    private readonly ISystemMessageClientService _systemInboxClientService;
    private readonly IContactMessageClientService _contactMessageClientService;
    private readonly IContactMessageAdminService _contactMessageService;

    public ProfileService(
        IAppUserRepository userRepository,
        UserManager<AppUser> userManager,
        ISystemMessageClientService systemMessageClientService,
        IContactMessageClientService contactMessageClientService,
        IContactMessageAdminService contactMessageService)
    {
        _userRepository = userRepository;
        _userManager = userManager;
        _systemInboxClientService = systemMessageClientService;
        _contactMessageClientService = contactMessageClientService;
        _contactMessageService = contactMessageService;
    }

    public async Task<ProfileViewModel?> GetProfileAsync(string userId)
    {
        var user = await _userRepository
            .Query()
            .FirstOrDefaultAsync(u => u.Id == userId);

        if (user == null)
            return null;

        var systemMessages = await _systemInboxClientService.GetUserMessagesAsync(userId);

        var roles = await _userManager.GetRolesAsync(user);
        var isAdmin = roles.Contains("Admin");
        var isManager = roles.Contains("Manager");

        List<ContactMessageDetailsViewModel> contactMessages = new List<ContactMessageDetailsViewModel>();

        if (isAdmin)
        {
            contactMessages = await _contactMessageService.GetAdminMessagesAsync(userId);
        }
        else if (!isManager)
        {
            contactMessages = await _contactMessageClientService.GetUserMessagesAsync(userId);
        }

        return new ProfileViewModel
        {
            Id = user.Id,
            Email = user.Email ?? string.Empty,
            FirstName = user.FirstName,
            LastName = user.LastName,
            Address = user.Address,
            SystemInbox = systemMessages?.ToList() ?? new List<SystemMessageViewModel>(),
            ContactMessages = contactMessages ?? new List<ContactMessageDetailsViewModel>()
        };
    }
}