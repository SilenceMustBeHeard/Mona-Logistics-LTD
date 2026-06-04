using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Services.Admin.Interfaces.Message;

public interface IContactMessageAdminService
{
    Task<List<ContactMessageDetailsViewModel>> GetAdminMessagesAsync(string adminId);

    Task<ContactMessageDetailsViewModel?> GetMessageDetailsAsync(Guid messageId, string adminId);

    Task RespondToMessageAsync(Guid messageId, string response, string adminId);

    Task MarkMessageAsReadAsync(Guid messageId, string adminId);

    Task<int> GetUnreadCountAsync(string adminId);

    Task MarkAllMessagesAsReadAsync(string adminId);
}