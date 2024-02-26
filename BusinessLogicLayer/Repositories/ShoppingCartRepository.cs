using BusinessLogicLayer.Base;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
	public class ShoppingCartRepository:GenericRepository<ShoppingCart>,IShoppingCartRepository
	{
		private readonly AppDbContext _appDbContext;
		public ShoppingCartRepository(AppDbContext appDbContext) : base(appDbContext)
		{
			_appDbContext = appDbContext;
		}
	}
}
