using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Mona_Logistics_LTD.Data;
using Mona_Logistics_LTD.Data.Models.Base;
using Mona_Logistics_LTD.Data.Repositories.Implementations.UnitOfWork;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.Account;
using Mona_Logistics_LTD.Data.Repositories.Interfaces.CRUD;
using Mona_Logistics_LTD.Services.Localizer.Implementations;
using Mona_Logistics_LTD.Services.Localizer.Interfaces;
using Mona_Logistics_LTD.Services.User.Implementations.Account;
using Mona_Logistics_LTD.Services.User.Implementations.Message;
using Mona_Logistics_LTD.Services.User.Interfaces.Account;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using Mona_Logistics_LTD.Web.Infrastructure.Extensions;
using Mona_Logistics_LTD.Web.ViewComponents;
using SendGrid;
using System.Globalization;
using Mona_Logistics_LTD.Data.Seeding;

namespace Mona_Logistics_LTD.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        if (builder.Environment.IsProduction())
        {
            builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: false);
        }

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Missing connection string");

        //  DATABASE - PostgreSQL 
        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(connectionString));

        //  IDENTITY 
        builder.Services.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = true;
            options.SignIn.RequireConfirmedEmail = true;

            // password settings
            options.Password.RequireDigit = true;
            options.Password.RequireNonAlphanumeric = true;
            options.Password.RequireUppercase = true;
            options.Password.RequireLowercase = true;
            options.Password.RequiredLength = 10;
            options.Password.RequiredUniqueChars = 4;

            // lockout settings
            options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
            options.Lockout.MaxFailedAccessAttempts = 5;
            options.Lockout.AllowedForNewUsers = true;

            // user settings
            options.User.RequireUniqueEmail = true;
            options.User.AllowedUserNameCharacters =
                "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        //  AUTHORIZATION 
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", p => p.RequireRole("Admin"));
            options.AddPolicy("ManagerPolicy", p => p.RequireRole("Manager"));
        });

        //  SESSION 
        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "Mona_Logistics_LTD.Session";
        });

        builder.Services.AddHttpContextAccessor();

        //  VIEW COMPONENTS 
        builder.Services.AddScoped<NavbarViewComponent>();
        builder.Services.AddScoped<AccountMenuViewComponent>();
        builder.Services.AddScoped<UnreadMessageBadgeViewComponent>();

        //  REPOSITORIES & SERVICES 
        builder.Services.RegisterRepositories(typeof(IAppUserRepository).Assembly);
        builder.Services.RegisterServices(typeof(IAccountService).Assembly);

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IContactMessageClientService, ContactMessageClientService>();
        builder.Services.AddScoped<ISystemMessageClientService, SystemMessageClientService>();

        //  SENDGRID 
        builder.Services.AddSingleton(sp =>
        {
            var apiKey = builder.Configuration["SendGrid:ApiKey"];
            if (string.IsNullOrEmpty(apiKey))
            {
                apiKey = builder.Configuration.GetValue<string>("SendGrid:ApiKey");
            }

            if (string.IsNullOrEmpty(apiKey))
            {
                throw new InvalidOperationException("SendGrid API Key is missing. Add it via: dotnet user-secrets set \"SendGrid:ApiKey\" \"YOUR_KEY\"");
            }

            return new SendGridClient(apiKey);
        });

        builder.Services.AddScoped<IEmailService, EmailService>();

        //  LOCALIZATION 
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<IJsonLocalizer, JsonLocalizerService>();

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
                new CultureInfo("bg-BG"),
                new CultureInfo("en-US")
            };

            options.DefaultRequestCulture = new RequestCulture("bg-BG");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;
            options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
        });

        //  MVC 
        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();

        //  HEALTH CHECKS 
        builder.Services.AddHealthChecks();

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        var app = builder.Build();

        //  SEEDING 
        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            await IdentitySeeder.SeedRolesAsync(roleManager);
            await IdentitySeeder.SeedAdminAsync(userManager);
            await IdentitySeeder.SeedManagerAsync(userManager);
        }

        //  STATIC FILES 
        var provider = new FileExtensionContentTypeProvider();
        provider.Mappings[".glb"] = "model/gltf-binary";

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }
        else
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseStatusCodePagesWithReExecute("/Error/{0}");

        app.UseHttpsRedirection();

        app.UseStaticFiles(new StaticFileOptions
        {
            ContentTypeProvider = provider
        });

        app.UseRouting();
        app.UseRequestLocalization();
        app.UseAuthentication();
        app.UseAuthorization();
        app.UseSession();

        //  ROUTING 
        app.MapGet("/", context =>
        {
            context.Response.Redirect("/Home/Index");
            return Task.CompletedTask;
        });

        app.MapControllerRoute(
            name: "areas",
            pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}");

        app.MapRazorPages();

        //  HEALTH CHECK 
        app.MapHealthChecks("/health");

        await app.RunAsync();
    }
}