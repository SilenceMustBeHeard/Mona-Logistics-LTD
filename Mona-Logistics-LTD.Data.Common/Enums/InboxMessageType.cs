using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Common.Enums;

public enum InboxMessageType
{
    UserToAdmin = 0,
    AdminToUser = 1,
    System = 2,
    Notification = 3
}
