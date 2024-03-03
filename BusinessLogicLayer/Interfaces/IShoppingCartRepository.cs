using BusinessLogicLayer.Base;
using BusinessLogicLayer.ViewModels;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Interfaces
{
	public interface IShoppingCartRepository:IGenericRepository<ShoppingCart>
	{
         Task AddToCart(string UserId, int id);
         void Add(string userid);
         Task DeleteFromCart(string UserId,int ProductId);
         Task<ShoppingCart> GetByIdIncludedOrderList(string userId);
    }
}
