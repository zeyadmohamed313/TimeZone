using BusinessLogicLayer.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.UnitOfWork
{
	public interface IUnitOfWork:IDisposable
	{
		IProductRepository ProductRepository { get; }
		public ICategoryRepository CategoryRepository { get; }
		public IFeedBackRepository FeedBackRepository { get; }
		public IShoppingCartRepository ShoppingCartRepository { get; }
		public IEmailSender EmailSender { get; }
		void Commit();
	}
}
