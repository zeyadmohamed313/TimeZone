using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public class ShoppingCartWithUserId:Specifications<ShoppingCart>
    {
        public ShoppingCartWithUserId(string UserId):base(e=>e.UserId==UserId) 
        {
            AddInclude(e => e.OrderList);
        }
    }
}
