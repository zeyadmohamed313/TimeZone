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
	public class OrderRepository:GenericRepository<Order>,IOrderRepository
	{
		private readonly AppDbContext _appDbContext;
		public OrderRepository(AppDbContext appDbContext):base(appDbContext)
		{
			_appDbContext = appDbContext;
		}
	}
}
