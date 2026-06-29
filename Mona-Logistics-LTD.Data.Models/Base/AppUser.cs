using Microsoft.AspNetCore.Identity;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Models.Messages;
using System.ComponentModel.DataAnnotations;

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

    public bool IsActive { get; set; } = true;

    public ICollection<Shipment> Shipments { get; set; }
    = new HashSet<Shipment>();

    // Navigation properties for messages
    public virtual ICollection<SystemMessage> ReceivedSystemMessages { get; set; }
        = new HashSet<SystemMessage>();

    public virtual ICollection<SystemMessage> SentSystemMessages { get; set; }
        = new HashSet<SystemMessage>();

    public virtual ICollection<ContactMessage> ReceivedContactMessages { get; set; }
        = new HashSet<ContactMessage>();

    public virtual ICollection<ContactMessage> SentContactMessages { get; set; }
        = new HashSet<ContactMessage>();
}