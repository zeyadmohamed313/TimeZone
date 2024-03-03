using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public string UserName {  get; set; }
        [Required]
        public string EmailAddress {  get; set; }
        [Required]
        public string PhoneNumber {  get; set; }
        [Required]
        public string Password {  get; set; }
        [Required]
        [Compare("Password")]
        public string ConfirmPassword {  get; set; }

        
    }
}
