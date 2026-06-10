using Mona_Logistics_LTD.Data.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;

public class SystemMessageCreateViewModel
{
    public string? ReceiverId { get; set; }
    public string? ReceiverName { get; set; }
    public required string Title { get; set; }
    public required string Description { get; set; }
    public InboxMessageType Type { get; set; }
    public List<UserSelectViewModel> AvailableUsers { get; set; } = new();
}