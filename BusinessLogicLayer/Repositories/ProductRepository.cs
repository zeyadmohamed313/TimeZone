﻿using BusinessLogicLayer.Base;
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
	public class ProductRepository : GenericRepository<Product>,IProductRepository
	{
		private readonly AppDbContext _appContext;
		public ProductRepository(AppDbContext appContext):base(appContext) 
		{
			_appContext = appContext;
		}
	}
}