using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Data.Configurations.Base;

public class InvoiceConfig : IEntityTypeConfiguration<Invoice>
{
    public void Configure(EntityTypeBuilder<Invoice> builder)
    {
        builder.HasOne(i => i.Shipment)
            .WithOne()
            .HasForeignKey<Invoice>(i => i.ShipmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}