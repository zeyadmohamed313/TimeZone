using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity; // this one 
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Identity
{
	public class ApplicationUser:IdentityUser
	{
		[Required]
		public string FirstName {  get; set; }
		[Required]
		public string LastName { get; set; }
		public string? Img {  get; set; }
		[EmailAddress]
		public string EmailAddress { get; set; }
		public int? ShoppingCartId {  get; set; }//made null
		[ForeignKey("ShoppingCartId")]
		public ShoppingCart? ShoppingCart { get; set; }
	}
}
