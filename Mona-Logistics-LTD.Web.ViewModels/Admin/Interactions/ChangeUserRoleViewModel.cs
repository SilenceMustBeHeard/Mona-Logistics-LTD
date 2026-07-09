using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.Admin.Interactions;


public class ChangeUserRoleViewModel
{
    public Guid UserId { get; set; }
    public string NewRole { get; set; } = string.Empty;

    public IEnumerable<string> AvailableRoles { get; set; } = new List<string>();
}