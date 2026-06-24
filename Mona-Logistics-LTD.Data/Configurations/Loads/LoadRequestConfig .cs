using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;

namespace Mona_Logistics_LTD.Data.Configurations.Loads;

public class LoadRequestConfig : IEntityTypeConfiguration<LoadRequest>
{
    public void Configure(EntityTypeBuilder<LoadRequest> builder)
    {
        builder.HasKey(lr => lr.Id);

        // Client (AppUser) - one-to-many, as LoadRequest  is the dependent side
        builder.HasOne(lr => lr.Client)
            .WithMany()
            .HasForeignKey(lr => lr.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        // offer - one-to-one relationship, as LoadRequest is the principal side
        builder.HasOne(lr => lr.Offer)
            .WithOne(o => o.LoadRequest)
            .HasForeignKey<Offer>(o => o.LoadRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indices for fast searching
        builder.HasIndex(lr => lr.Status);
        builder.HasIndex(lr => lr.ClientId);
        builder.HasIndex(lr => lr.PreferredPickupDate);

        // Default values for new requests
        builder.Property(lr => lr.Status)
            .HasDefaultValue(LoadRequestStatus.Pending);

        builder.Property(lr => lr.CreatedAt)
            .HasDefaultValueSql("GETUTCDATE()");

        // Check constraints for numeric fields
        builder.ToTable(tb => tb.HasCheckConstraint("CK_LoadRequest_WeightKg", "WeightKg > 0"));
        builder.ToTable(tb => tb.HasCheckConstraint("CK_LoadRequest_VolumeM3", "VolumeM3 > 0"));
    }
}