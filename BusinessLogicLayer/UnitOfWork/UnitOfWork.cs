using BusinessLogicLayer.Interfaces;
using DataAccessLayer.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UnitOfWork
{
	public class UnitOfWork : IUnitOfWork
	{
		public IProductRepository ProductRepository { get; private set; }
		public IOrderRepository OrderRepository { get; private set; }
		public ICategoryRepository CategoryRepository { get; private set; }
		public IShoppingCartRepository ShoppingCartRepository { get; private set; }

		private readonly AppDbContext _appDbContext;
		// Inject the parameters
		public UnitOfWork(AppDbContext appDbContext , IProductRepository productRepository , 
			IOrderRepository orderRepository , ICategoryRepository categoryRepository , IShoppingCartRepository shoppingCartRepository)
		{
			_appDbContext = appDbContext;
			ProductRepository = productRepository;
			OrderRepository = orderRepository;
			CategoryRepository = categoryRepository;
			ShoppingCartRepository = shoppingCartRepository;
		}
		public  void Commit()
		{
			 _appDbContext.SaveChanges();
		}

		public void Dispose()
		{
			_appDbContext.Dispose();
		}
	}
}
