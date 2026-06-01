using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Loads;

namespace Mona_Logistics_LTD.Data.Configurations.Loads;

public class ShipmentConfig : IEntityTypeConfiguration<Shipment>
{
    public void Configure(EntityTypeBuilder<Shipment> builder)
    {
        builder.HasOne(s => s.Client)
            .WithMany(u => u.Shipments)
            .HasForeignKey(s => s.ClientId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(s => s.CargoItems)
            .WithOne(c => c.Shipment)
            .HasForeignKey(c => c.ShipmentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}