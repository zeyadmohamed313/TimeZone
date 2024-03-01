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
        public FilterProductsAsc(int? CategoryId, decimal? minprice = 0, decimal? maxprice = 20000)
       : base(p => CategoryId != null ? (p.CategoryId == CategoryId):(p.CategoryId>-1)&& minprice!=null?(p.Price >= minprice):(p.Price>=0)
         && maxprice!=null ? (p.Price <= maxprice):(p.Price<=20000))
        {
            AddOrderBy(p => p.Price);
        }
    }
}
