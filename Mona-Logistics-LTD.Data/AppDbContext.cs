using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Models.Loads;
using Mona_Logistics_LTD.Data.Models.Messages;
using Mona_Logistics_LTD.Data.Models.Routes;
using Mona_Logistics_LTD.Data.Models.Trucks;

namespace Mona_Logistics_LTD.Data;

public class AppDbContext : IdentityDbContext<AppUser>
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }

    public DbSet<AppUser> AppUsers => Set<AppUser>();
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<Truck> Trucks { get; set; }
    public DbSet<Driver> Drivers { get; set; }
    public DbSet<Route> Routes { get; set; }
    public DbSet<Trip> Trips { get; set; }
    public DbSet<Invoice> Invoices { get; set; }
    public DbSet<ContactMessage> ContactMessages { get; set; }
    public DbSet<SystemMessage> SystemMessages { get; set; }
    public DbSet<LoadRequest> LoadRequests { get; set; }
    public DbSet<Offer> Offers { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        // Soft delete filters
        builder.Entity<ContactMessage>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<SystemMessage>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<Shipment>().HasQueryFilter(s => !s.IsDeleted);
        builder.Entity<Invoice>().HasQueryFilter(i => !i.IsDeleted);
        builder.Entity<Cargo>().HasQueryFilter(c => !c.IsDeleted);
        builder.Entity<Trip>().HasQueryFilter(t => !t.IsDeleted);
        builder.Entity<LoadRequest>().HasQueryFilter(l => !l.IsDeleted);
        builder.Entity<Offer>().HasQueryFilter(o => !o.IsDeleted);
    }
}