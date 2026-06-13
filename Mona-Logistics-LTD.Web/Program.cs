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
using Mona_Logistics_LTD.Services.User.Implementations.Account;
using Mona_Logistics_LTD.Services.User.Implementations.Message;
using Mona_Logistics_LTD.Services.User.Interfaces.Account;
using Mona_Logistics_LTD.Services.User.Interfaces.Message;
using Mona_Logistics_LTD.Web.Infrastructure.Extensions;
using Mona_Logistics_LTD.Web.ViewComponents;
using SendGrid;
using System.Globalization;

namespace Mona_Logistics_LTD.Web;

public class Program
{
    public static async Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Missing connection string");

        builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        builder.Services.AddDefaultIdentity<AppUser>(options =>
        {
            options.SignIn.RequireConfirmedAccount = false;
            options.Password.RequireDigit = false;
            options.Password.RequireNonAlphanumeric = false;
            options.Password.RequireUppercase = false;
            options.Password.RequireLowercase = false;
        })
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<AppDbContext>();

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("AdminPolicy", p => p.RequireRole("Admin"));
            options.AddPolicy("ManagerPolicy", p => p.RequireRole("Manager"));
        });

        builder.Services.AddDistributedMemoryCache();
        builder.Services.AddSession(options =>
        {
            options.IdleTimeout = TimeSpan.FromMinutes(30);
            options.Cookie.HttpOnly = true;
            options.Cookie.IsEssential = true;
            options.Cookie.Name = "Mona_Logistics_LTD.Session";
        });

        builder.Services.AddHttpContextAccessor();

        // Register ViewComponents
        builder.Services.AddScoped<NavbarViewComponent>();
        builder.Services.AddScoped<AccountMenuViewComponent>();
        builder.Services.AddScoped<UnreadMessageBadgeViewComponent>();



        builder.Services.RegisterRepositories(typeof(IAppUserRepository).Assembly);
        builder.Services.RegisterServices(typeof(IAccountService).Assembly);

        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddScoped<IContactMessageClientService, ContactMessageClientService>();

        builder.Services.AddScoped<ISystemMessageClientService, SystemMessageClientService>();
      
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

        builder.Services.AddControllersWithViews();
        builder.Services.AddRazorPages();


        // ========== LOCALIZATION ==========
        builder.Services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        builder.Services.AddMvc()
            .AddViewLocalization(Microsoft.AspNetCore.Mvc.Razor.LanguageViewLocationExpanderFormat.Suffix)
            .AddDataAnnotationsLocalization();

        builder.Services.Configure<RequestLocalizationOptions>(options =>
        {
            var supportedCultures = new[]
            {
        new CultureInfo("bg-BG"),  // Bulgarian
        new CultureInfo("en-US")   // English
    };

            options.DefaultRequestCulture = new RequestCulture("bg-BG");
            options.SupportedCultures = supportedCultures;
            options.SupportedUICultures = supportedCultures;

            // Cookie provider - saves the user's language preference in a cookie
            options.RequestCultureProviders.Insert(0, new CookieRequestCultureProvider());
        });

        builder.Services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        var app = builder.Build();

        using (var scope = app.Services.CreateScope())
        {
            var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
            var userManager = scope.ServiceProvider.GetRequiredService<UserManager<AppUser>>();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            await context.Database.MigrateAsync();

            //await IdentitySeeder.SeedRolesAsync(roleManager);
            //await IdentitySeeder.SeedAdminAsync(userManager);
            //await IdentitySeeder.SeedManagerAsync(userManager);

            //if (!await context.Categories.AnyAsync())
            //    await DbSeeder.SeedAllAsync(context);
        }

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
        //app.MapControllers();

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

        await app.RunAsync();
    }
}