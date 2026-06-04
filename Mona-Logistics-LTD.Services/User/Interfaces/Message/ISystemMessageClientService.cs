using Mona_Logistics_LTD.Web.ViewModels.User.Account;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.User.Interfaces.Message;

public interface ISystemMessageClientService
{

    Task<List<SystemMessageViewModel>> GetUserMessagesAsync(string userId);
    Task<SystemMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);
    Task<int> GetUnreadCountAsync(string userId);
    Task MarkAsReadAsync(Guid messageId, string userId);
}