using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data.Models.Trucks;
using Mona_Logistics_LTD.Data.Models.Routes;
using Mona_Logistics_LTD.Data.Models.Loads;
using System.Text.Json;

namespace Mona_Logistics_LTD.Data.Seeding;

public static class LogisticsSeeder
{
    private static readonly SemaphoreSlim _semaphore = new(1, 1);

    // ==================== TRUCKS ====================
    public static async Task SeedTrucksAsync(AppDbContext context)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (await context.Trucks.AnyAsync())
            {
                Console.WriteLine("Trucks already exist. Skipping.");
                return;
            }

            var jsonPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "trucks.json"
            );

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"⚠️ trucks.json not found at: {jsonPath}");
                return;
            }

            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var trucks = JsonSerializer.Deserialize<List<Truck>>(json, options)
                ?? throw new Exception("trucks.json is empty or invalid");

            foreach (var truck in trucks)
            {
                if (truck.Id == Guid.Empty)
                    truck.Id = Guid.NewGuid();

                truck.CreatedAt = DateTime.UtcNow;
                truck.IsDeleted = false;
            }

            await context.Trucks.AddRangeAsync(trucks);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Seeded {trucks.Count} trucks");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // ==================== DRIVERS ====================
    public static async Task SeedDriversAsync(AppDbContext context)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (await context.Drivers.AnyAsync())
            {
                Console.WriteLine("Drivers already exist. Skipping.");
                return;
            }

            var jsonPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "drivers.json"
            );

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"⚠️ drivers.json not found at: {jsonPath}");
                return;
            }

            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var drivers = JsonSerializer.Deserialize<List<Driver>>(json, options)
                ?? throw new Exception("drivers.json is empty or invalid");

            foreach (var driver in drivers)
            {
                if (driver.Id == Guid.Empty)
                    driver.Id = Guid.NewGuid();

                driver.CreatedAt = DateTime.UtcNow;
                driver.IsDeleted = false;
            }

            await context.Drivers.AddRangeAsync(drivers);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Seeded {drivers.Count} drivers");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // ==================== ROUTES ====================
    public static async Task SeedRoutesAsync(AppDbContext context)
    {
        await _semaphore.WaitAsync();
        try
        {
            if (await context.Routes.AnyAsync())
            {
                Console.WriteLine("Routes already exist. Skipping.");
                return;
            }

            var jsonPath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "wwwroot",
                "data",
                "routes.json"
            );

            if (!File.Exists(jsonPath))
            {
                Console.WriteLine($"⚠️ routes.json not found at: {jsonPath}");
                return;
            }

            var json = await File.ReadAllTextAsync(jsonPath);
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
            var routes = JsonSerializer.Deserialize<List<Models.Routes.Route>>(json, options)
                ?? throw new Exception("routes.json is empty or invalid");

            foreach (var route in routes)
            {
                if (route.Id == Guid.Empty)
                    route.Id = Guid.NewGuid();

                route.CreatedAt = DateTime.UtcNow;
                route.IsDeleted = false;
            }

            await context.Routes.AddRangeAsync(routes);
            await context.SaveChangesAsync();
            Console.WriteLine($"✅ Seeded {routes.Count} routes");
        }
        finally
        {
            _semaphore.Release();
        }
    }

    // ==================== ALL IN ONE ====================
    public static async Task SeedAllAsync(AppDbContext context)
    {
        Console.WriteLine("🌱 Starting logistics seeding...");

        await SeedTrucksAsync(context);
        await SeedDriversAsync(context);
        await SeedRoutesAsync(context);


        Console.WriteLine("✅ Logistics seeding completed!");
    }
}