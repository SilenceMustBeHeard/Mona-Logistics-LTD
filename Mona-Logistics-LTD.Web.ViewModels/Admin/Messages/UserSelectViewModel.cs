using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;

public class UserSelectViewModel
{
    public string Id { get; set; } = null!;
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}