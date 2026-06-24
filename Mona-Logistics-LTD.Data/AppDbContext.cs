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

    public DbSet<Shipment> Shipments { get; set; } = null!;

    public DbSet<Cargo> Cargos { get; set; } = null!;

    public DbSet<Truck> Trucks { get; set; } = null!;

    public DbSet<LoadRequest> LoadRequests { get; set; } = null!;

    public DbSet<LoadRequestDocument> LoadRequestDocuments { get; set; } = null!;
    public DbSet<Offer> Offers { get; set; } = null!;


    public DbSet<Driver> Drivers { get; set; } = null!;

    public DbSet<Route> Routes { get; set; } = null!;

    public DbSet<Trip> Trips { get; set; } = null!;

    public DbSet<Invoice> Invoices { get; set; } = null!;

    public DbSet<ContactMessage> ContactMessages { get; set; } = null!;

    public DbSet<SystemMessage> SystemMessages { get; set; } = null!;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        builder.Entity<ContactMessage>().HasQueryFilter(m => !m.IsDeleted);
        builder.Entity<SystemMessage>().HasQueryFilter(m => !m.IsDeleted);

        builder.Entity<Invoice>().HasQueryFilter(i => !i.IsDeleted);

        builder.Entity<Trip>().HasQueryFilter(t => !t.IsDeleted);
        builder.Entity<Route>().HasQueryFilter(r => !r.IsDeleted);
        builder.Entity<Shipment>().HasQueryFilter(s => !s.IsDeleted);
        builder.Entity<Cargo>().HasQueryFilter(c => !c.IsDeleted);

        builder.Entity<Truck>().HasQueryFilter(t => !t.IsDeleted);
        builder.Entity<Driver>().HasQueryFilter(d => !d.IsDeleted);
        builder.Entity<LoadRequest>().HasQueryFilter(lr => !lr.IsDeleted);
        builder.Entity<LoadRequestDocument>().HasQueryFilter(lrd => !lrd.IsDeleted);
        builder.Entity<Offer>().HasQueryFilter(o => !o.IsDeleted);
    }
}
