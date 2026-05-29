using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Base;

public class AppUser : IdentityUser
{
    public string FullName => $"{FirstName?.Trim()} {LastName?.Trim()}".Trim();

    [Required]
    public string FirstName { get; set; } = null!;

    [Required]
    public string LastName { get; set; } = null!;

    public string? Address { get; set; }

    public string? AlternateEmail { get; set; }

    //// Navigation properties for messages
    //public virtual ICollection<SystemInboxMessage> ReceivedSystemMessages { get; set; }
    //    = new HashSet<SystemInboxMessage>();

    //public virtual ICollection<SystemInboxMessage> SentSystemMessages { get; set; }
    //    = new HashSet<SystemInboxMessage>();

    //public virtual ICollection<ContactMessage> ReceivedContactMessages { get; set; }
    //    = new HashSet<ContactMessage>();

    //public virtual ICollection<ContactMessage> SentContactMessages { get; set; }
    //    = new HashSet<ContactMessage>();
}