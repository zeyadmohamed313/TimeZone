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
		public ICategoryRepository CategoryRepository { get; private set; }
		public IShoppingCartRepository ShoppingCartRepository { get; private set; }
		public IFeedBackRepository FeedBackRepository { get; private set; }
		public IEmailSender EmailSender { get; private set; }

		private readonly AppDbContext _appDbContext;
		// Inject the parameters
		public UnitOfWork(AppDbContext appDbContext , IProductRepository productRepository , 
			  ICategoryRepository categoryRepository ,
			  IShoppingCartRepository shoppingCartRepository ,
			  IFeedBackRepository feedBackRepository,
			  IEmailSender emailSender)
		{
			_appDbContext = appDbContext;
			ProductRepository = productRepository;
			CategoryRepository = categoryRepository;
			ShoppingCartRepository = shoppingCartRepository;
			FeedBackRepository = feedBackRepository;
			EmailSender = emailSender;
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
