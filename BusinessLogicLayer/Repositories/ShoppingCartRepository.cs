using BusinessLogicLayer.Base;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Specifications;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using DataAccessLayer.Models.Identity;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BusinessLogicLayer.ViewModels;

namespace BusinessLogicLayer.Repositories
{
	public class ShoppingCartRepository:GenericRepository<ShoppingCart>,IShoppingCartRepository
	{
		private readonly AppDbContext _appDbContext;
		public ShoppingCartRepository(AppDbContext appDbContext ) : base(appDbContext)
		{
			_appDbContext = appDbContext;
		}
		
		public void Add(string userid)
		{ 
			_appDbContext.ShoppingCarts.Add( new ShoppingCart() { UserId= userid } );
			
		}



        public async Task AddToCart(string UserId ,int id)
		{
			var product = await _appDbContext.products.FirstOrDefaultAsync(p=>p.Id==id);
			var UserShoppingCart = await _appDbContext.ShoppingCarts.Include(e=>e.OrderList).FirstOrDefaultAsync(x => x.UserId == UserId);
			UserShoppingCart.OrderList.Add(product); // note that this is not working with Admin
		}

        public async Task<ShoppingCart> GetByIdIncludedOrderList(string userId)
		{
			return  await _appDbContext.ShoppingCarts.Include(e=>e.OrderList).FirstOrDefaultAsync(e=>e.UserId==userId);
		}

		public async Task DeleteFromCart(string UserId , int ProductId)
		{
			var ShoppingCart =await _appDbContext.ShoppingCarts.Include(e=>e.OrderList).FirstOrDefaultAsync(s=>s.UserId==UserId);
            ShoppingCart.OrderList.Remove(ShoppingCart.OrderList.FirstOrDefault(e => e.Id == ProductId));
		}

    }
}
