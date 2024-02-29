using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Specifications
{
    public class MinMaxPriceSpecifications :Specifications<Product>
    {
        public MinMaxPriceSpecifications(int minPrice , int maxPrice) 
            :base(p=>p.Price<=maxPrice && p.Price>=minPrice)
        {

        }
    }
}
