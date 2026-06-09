using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Messages;

namespace Mona_Logistics_LTD.Data.Configurations.Message;

public abstract class BaseMessageConfig<T> : IEntityTypeConfiguration<T> where T : BaseMessage
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(m => m.Id);

    }
}