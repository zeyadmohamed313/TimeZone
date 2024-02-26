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
	public class Order
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public decimal TotalPrice { get; set; }
		[Required]
		public DateTime? OrderDate { get; set; }
		[Required]
		public string UserId {  get; set; }

		[ForeignKey("UserId")]
		public ApplicationUser User { get; set; }
		[Required]
		public int ShoppingCartId {  get; set; }
		[ForeignKey("ShoppingCartId")]
		public ShoppingCart ShoppingCart { get; set; }
		public List<OrderItem>? OrderItems { get; set; }
	}
}
