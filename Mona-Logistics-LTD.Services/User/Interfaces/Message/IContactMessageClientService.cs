using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account.Messages;
using System.Security.Claims;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Message;

public interface IContactMessageClientService
{
    Task SendContactMessageAsync(ContactMessageCreateViewModel model, ClaimsPrincipal userPrincipal);

    Task<List<ContactMessageDetailsViewModel>> GetUserMessagesAsync(string userId);

    Task<ContactMessageDetailsViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);

    Task<int> GetUserUnreadResponsesCountAsync(string userId);

    Task<bool?> MarkAsReadAsync(Guid messageId, string userId);
}