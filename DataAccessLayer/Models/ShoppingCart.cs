using DataAccessLayer.Models.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class ShoppingCart
	{
		[Required]
		public int Id { get; set; }
		public decimal TotalPrice {  get; set; }
		[Required]
		public string  UserId {  get; set; }
		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; }
		
		public List<Product>? OrderList { get; set; }
		public decimal GetTotalPrice => OrderList!=null? OrderList
                    .Sum(orderItem => orderItem.Price):0;    
	}
	
}
