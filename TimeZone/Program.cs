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

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>(option => option.UseSqlServer(builder.Configuration.GetConnectionString("cs")));
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IShoppingCartRepository, ShoppingCartRepository>();
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

app.UseAuthorization();

app.MapControllerRoute(
	name: "default",
	pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
