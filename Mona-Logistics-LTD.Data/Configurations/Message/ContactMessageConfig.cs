using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Messages;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mona_Logistics_LTD.Data.Configurations.Message;

public class ContactMessageConfig : BaseMessageConfig<ContactMessage>
{
    public override void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        base.Configure(builder);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedContactMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentContactMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.RespondedBy)
            .WithMany()
            .HasForeignKey(m => m.RespondedById)
            .OnDelete(DeleteBehavior.Restrict);
    }
}