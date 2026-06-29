using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Messages;

namespace Mona_Logistics_LTD.Data.Configurations.Message;

public class SystemMessageConfig : BaseMessageConfig<SystemMessage>
{
    public override void Configure(EntityTypeBuilder<SystemMessage> builder)
    {
        base.Configure(builder);

        builder.HasOne(m => m.Receiver)
            .WithMany(u => u.ReceivedSystemMessages)
            .HasForeignKey(m => m.ReceiverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(m => m.Sender)
            .WithMany(u => u.SentSystemMessages)
            .HasForeignKey(m => m.SenderId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}