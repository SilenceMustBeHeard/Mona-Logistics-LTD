using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Common.Enums;
using Mona_Logistics_LTD.Data.Models.Loads;

namespace Mona_Logistics_LTD.Data.Configurations.Loads;

public class OfferConfig : IEntityTypeConfiguration<Offer>
{
    public void Configure(EntityTypeBuilder<Offer> builder)
    {
        builder.HasKey(o => o.Id);

        // LoadRequest - one-to-one relationship, as Offer is the dependent side
        builder.HasOne(o => o.LoadRequest)
            .WithOne(lr => lr.Offer)
            .HasForeignKey<Offer>(o => o.LoadRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        //  Creator (AppUser) - one-to-many
        builder.HasOne(o => o.CreatedBy)
            .WithMany()
            .HasForeignKey(o => o.CreatedById)
            .OnDelete(DeleteBehavior.Restrict);

        // Indices
        builder.HasIndex(o => o.LoadRequestId).IsUnique();
        builder.HasIndex(o => o.Status);
        builder.HasIndex(o => o.ValidUntil);

        // Default values
        builder.Property(o => o.Status)
            .HasDefaultValue(OfferStatus.Pending);

        builder.Property(o => o.CreatedAt)
               .HasDefaultValueSql("CURRENT_TIMESTAMP");

        // Price must be positive
        builder.ToTable(tb => tb.HasCheckConstraint("CK_Offer_Price", "Price >= 0"));
        builder.ToTable(tb => tb.HasCheckConstraint("CK_Offer_Discount", "Discount >= 0 AND Discount <= Price"));
    }
}