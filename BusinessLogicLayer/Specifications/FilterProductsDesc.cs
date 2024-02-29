using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public class FilterProductsDesc :Specifications<Product>
    {
        public FilterProductsDesc(int? CategoryId , decimal? minprice , decimal? maxprice)
        :base(p=>p.CategoryId==CategoryId && p.Price>=minprice && p.Price<=maxprice) 
        {
            AddOrderByDesc(p => p.Price);
        }
    }
}
