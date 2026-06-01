using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Routes;

namespace Mona_Logistics_LTD.Data.Configurations.Routes;

public class TripConfig : IEntityTypeConfiguration<Trip>
{
    public void Configure(EntityTypeBuilder<Trip> builder)
    {
        builder.HasOne(t => t.Truck)
            .WithMany(t => t.Trips)
            .HasForeignKey(t => t.TruckId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Driver)
            .WithMany(d => d.Trips)
            .HasForeignKey(t => t.DriverId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Route)
            .WithMany(r => r.Trips)
            .HasForeignKey(t => t.RouteId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(t => t.Shipment)
            .WithMany(s => s.Trips)
            .HasForeignKey(t => t.ShipmentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}