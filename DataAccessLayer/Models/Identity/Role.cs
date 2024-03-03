using Azure;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models.Identity
{
    public class Role:IdentityRole<int>
    {
        public Role() { }
        public Role(string Role):base(Role) 
        { }
        
    }
}
