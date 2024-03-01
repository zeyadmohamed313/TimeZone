﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.ViewModels
{
    public class LoginViewModel
    {
        [Required]
        public string Name {  get; set; }
        [Required(ErrorMessage ="Password Must Contain at Least 8 characters")]
        [MinLength(8)]
        
        public string Password { get; set; }
        public bool RememberMe {  get; set; }
    }
}
