using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
	public class Product
	{
		[Required]
		public int Id { get; set; }
		[Required]
		public string Name { get; set; }
		[Required]
		public string? Description { get; set; }
		[Required]
		public int StockQuantity {  get; set; }
		[Required]
		public decimal Price { get; set; }
		[Required]
		public bool IsPopularNow { get; set; } = false;
		[Required]
		public byte[] Img {  get; set; }
		public DateTime InsertionTime { get; set; }
		[Required]
		public int CategoryId {  get; set; }

		[ForeignKey(nameof(CategoryId))]
		public Category Category { get; set; }
		[Required]
		public int ShoppingCartId {  get; set; }
		[ForeignKey("ShoppingCartId")]
		public List<ShoppingCart>? ShoppingCarts { get; set; }
	}
}
