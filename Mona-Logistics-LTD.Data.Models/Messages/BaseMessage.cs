using Mona_Logistics_LTD.Data.Models.Base;
using System;
using Mona_Logistics_LTD.Data.Common.Enums;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mona_Logistics_LTD.Data.Models.Messages;


public abstract class BaseMessage : BaseDeletableEntity
{
    [Required]
    public  string ReceiverId { get; set; }

    public string? SenderId { get; set; }

    [Required]
    public InboxMessageType Type { get; set; }

    public bool IsRead { get; set; }
    public DateTime? ReadAt { get; set; }

    // Navigation properties - using required keyword (C# 11+)
    [Required]
    public virtual  AppUser Receiver { get; set; } = null!;

    public virtual AppUser? Sender { get; set; }
}
