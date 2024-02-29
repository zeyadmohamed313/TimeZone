using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class ShopViewModel
    {
        public int CategoryId {  get; set; }
        public decimal MinPrice {  get; set; }
        public decimal MaxPrice { get; set; }
        
        public bool sorted {  get; set; }

    }
}
