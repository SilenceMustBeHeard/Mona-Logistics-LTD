using Mona_Logistics_LTD.Data.Models.Messages;

using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account.Messages;

namespace Mona_Logistics_LTD.Services.Admin.Interfaces.Message;

public interface ISystemMessageAdminService
{
    Task MarkMessageAsReadAsync(Guid messageId, string userId);

    Task<int> GetUnreadCountAsync(string userId);

    Task<SystemMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);

    Task<List<SystemMessageViewModel>> GetAdminMessagesAsync(string adminId);

    Task CreateMessageAsync(SystemMessage message);

    Task<List<SystemMessageViewModel>> GetUserMessagesAsync(string userId);

    Task<SystemMessageCreateViewModel> GetCreateViewModelAsync(string? userId = null);

    Task<(bool Success, string ErrorMessage)> CreateMessageAsync(
        SystemMessageCreateViewModel model,
        string adminId);
}