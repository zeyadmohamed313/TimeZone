using DataAccessLayer.Models;
using DataAccessLayer.Models.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace DataAccessLayer.Context
{
    public class AppDbContext : IdentityDbContext<ApplicationUser>
	{
		public AppDbContext()
		{
		}
		public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
		{

		}
		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.
				UseSqlServer("Data Source=DESKTOP-8QKV55J\\SQLEXPRESS;Initial Catalog=TimeZone;Integrated Security=True;Connect Timeout=30;Encrypt=False;Trust Server Certificate=False;Application Intent=ReadWrite;Multi Subnet Failover=False");
		}
		public DbSet<Product> products {  get; set; }
		public DbSet<Category> Categories { get; set; }
		public DbSet<ShoppingCart> ShoppingCarts { get; set; }
		public DbSet<ApplicationUser> Users {  get; set; }
		public DbSet<FeedBack> FeedBacks { get; set; }
	}
}
