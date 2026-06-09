using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;


public class AdminInboxViewModel
{

    public List<ContactMessageDetailsViewModel> ContactMessages { get; set; } = new();
}