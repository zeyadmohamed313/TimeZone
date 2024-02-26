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
	public class OrderItem
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public int OrderId {  get; set; }
		[Required]
		public int ProductId {  get; set; }
		[ForeignKey("ProductId")]
		public Product Product { get; set; }
		[ForeignKey("OrderId")]
		public Order Order { get; set; }

	}
}
