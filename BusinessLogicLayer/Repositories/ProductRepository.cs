using BusinessLogicLayer.Base;
using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Repositories
{
	public class ProductRepository : GenericRepository<Product>,IProductRepository
	{
		private readonly AppDbContext _appContext;
		public ProductRepository(AppDbContext appContext):base(appContext) 
		{
			_appContext = appContext;
		}

        public async Task<List<Product>>? GetPopularProducts()
		{
			return await _appContext.products.Where(p=>p.IsPopularNow==true).ToListAsync();
		}

    }
}
