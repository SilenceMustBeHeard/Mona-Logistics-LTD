using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data;
using Mona_Logistics_LTD.Data.Models.Base;

namespace Mona_Logistics_LTD.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);


        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddIdentity<AppUser, IdentityRole>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false; options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
            options.Password.RequiredLength = 6;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddDefaultTokenProviders();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", p => p.RequireRole("Admin"));
            options.AddPolicy("DriverPolicy", p => p.RequireRole("Driver"));
        });

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
        });

        builder.Services.AddHttpContextAccessor();

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        var app = builder.Build();


        using (var scope = app.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            try
            {
                var context = services.GetRequiredService<AppDbContext>();
                var userManager = services.GetRequiredService<UserManager<AppUser>>();
                var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

                await context.Database.MigrateAsync();

            }
            catch (Exception ex)
            {
                var logger = services.GetRequiredService<ILogger<Program>>();
                logger.LogError(ex, "An error occurred while seeding the database.");
            }
        }

        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
            app.UseDeveloperExceptionPage();
        }
        else
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();
        app.UseRouting();

        app.UseAuthentication(); app.UseAuthorization();
        app.UseSession();
        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        await app.RunAsync();
    }
}