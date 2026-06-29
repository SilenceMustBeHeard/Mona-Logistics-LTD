using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Mona_Logistics_LTD.Data.Models.Routes;

namespace Mona_Logistics_LTD.Data.Configurations.Routes;

public class RouteConfig : IEntityTypeConfiguration<Route>
{
    public void Configure(EntityTypeBuilder<Route> builder)
    {
    }
}