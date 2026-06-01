using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Trucks;

namespace Mona_Logistics_LTD.Data.Configurations.Trucks;

public class TruckConfig : IEntityTypeConfiguration<Truck>
{
    public void Configure(EntityTypeBuilder<Truck> builder)
    {
       
        builder.HasIndex(t => t.RegistrationNumber).IsUnique();
    }
}