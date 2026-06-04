using Mona_Logistics_LTD.Data.Models.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account;
using System;
using System.Collections.Generic;
using System.Text;


namespace Mona_Logistics_LTD.Services.Core.Service.Admin.Interfaces.Message;

public interface ISystemMessageAdminService
{
    Task MarkMessageAsReadAsync(Guid messageId, string userId);

    Task<int> GetUnreadCountAsync(string userId);

    Task<SystemMessageViewModel?> GetMessageDetailsAsync(Guid messageId, string userId);

    Task<List<SystemMessageViewModel>> GetAdminMessagesAsync(string adminId);

    Task CreateMessageAsync(SystemMessage message);

    Task<List<SystemMessageViewModel>> GetUserMessagesAsync(string userId);
}