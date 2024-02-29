using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public class FilterProductsAsc : Specifications<Product>
    {
        public FilterProductsAsc(int? CategoryId, decimal? minprice, decimal? maxprice)
       : base(p => p.CategoryId == CategoryId && p.Price >= minprice && p.Price <= maxprice)
        {
            AddOrderBy(p => p.Price);
        }
    }
}
