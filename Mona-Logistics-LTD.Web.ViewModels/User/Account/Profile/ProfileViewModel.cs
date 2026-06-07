using Mona_Logistics_LTD.Web.ViewModels.Admin.Messages;
using Mona_Logistics_LTD.Web.ViewModels.User.Account.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Web.ViewModels.User.Account.Profile;


public class ProfileViewModel
{
    public string Id { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? FirstName { get; set; } = null!;
    public string? LastName { get; set; } = null!;

    public string? Address { get; set; }

    public string AlternateEmail { get; set; } = null!;


    public IEnumerable<SystemMessageViewModel> SystemInbox { get; set; }
        = new List<SystemMessageViewModel>();

    public IEnumerable<ContactMessageDetailsViewModel> ContactMessages { get; set; }
        = new List<ContactMessageDetailsViewModel>();
}