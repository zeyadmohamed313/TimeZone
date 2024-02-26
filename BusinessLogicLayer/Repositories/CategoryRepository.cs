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
	public class CategoryRepository : GenericRepository<Category>, ICategoryRepository

    {
		private readonly AppDbContext _appDbContext;
		public CategoryRepository(AppDbContext appDbContext) : base(appDbContext)
		{
			_appDbContext = appDbContext;
		}
        public Category GetCategoryWithProducts(int id)
		{
			return _appDbContext.Categories.Include(c => c.Products).FirstOrDefault(e => e.Id == id);
		}

    }
}
