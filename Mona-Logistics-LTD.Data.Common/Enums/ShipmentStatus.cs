using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Common.Enums;

public enum ShipmentStatus
{
    Pending = 0,
    Confirmed = 1,
    Loading = 2,
    InTransit = 3,
    Delivered = 4,
    Cancelled = 5
}