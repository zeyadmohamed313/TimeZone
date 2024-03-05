using DataAccessLayer.Context;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.UnitOfWork;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Repositories;
using BusinessLogicLayer.Base;
using BusinessLogicLayer.AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Google;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ICategoryRepository, CategoryRepository>();
builder.Services.AddScoped<IFeedBackRepository, FeedBackRepository>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();
builder.Services.AddScoped<IEmailSender,EmailSenderRepository>();
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // Configure Identity options if needed
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 8;
    // Add other options...
})
.AddEntityFrameworkStores<AppDbContext>() // Use your DbContext
.AddDefaultTokenProviders();

builder.Services.AddAuthentication(
    options =>{
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

    }
    ).
    AddCookie(options =>
    {
        options.Cookie.HttpOnly = true;
        options.ExpireTimeSpan = TimeSpan.FromMinutes(60);
        options.SlidingExpiration = true;
        options.AccessDeniedPath = "/Account/AccessDenied";
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.ReturnUrlParameter = "returnUrl";
    }
    ).AddMicrosoftAccount(options =>
    {
        IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Microsoft");

        options.ClientId = googleAuthSection["ClientId"];
        options.ClientSecret = googleAuthSection["ClientSecret"];
    })

                 .AddGoogle(options =>
                 {
                     IConfigurationSection googleAuthSection = builder.Configuration.GetSection("Authentication:Google");

                     options.ClientId = googleAuthSection["ClientId"];
                     options.ClientSecret = googleAuthSection["ClientSecret"];
                 });

using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();

    // Seed roles
    await SeedRoles(roleManager);

    // Seed users with roles
    await SeedUsers(userManager, roleManager);
}

async Task SeedRoles(RoleManager<IdentityRole> roleManager)
{
    // Check if roles exist, if not, create them
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }

    if (!await roleManager.RoleExistsAsync("User"))
    {
        await roleManager.CreateAsync(new IdentityRole("User"));
    }
}

async Task SeedUsers(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
{
    // Check if the admin user exists, if not, create it
    var adminUser = await userManager.FindByNameAsync("admin");

    if (adminUser == null)
    {
        adminUser = new ApplicationUser
        {
            UserName = "admin",
            FirstName="admin",
            LastName="admin",
            EmailAddress = "admin@example.com",
            // Add other properties as needed
        };

        await userManager.CreateAsync(adminUser, "Admin@123"); // Replace with a secure password

        // Assign the "Admin" role to the admin user
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }

   
    
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
	app.UseExceptionHandler("/Home/Error");
	// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
	app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
