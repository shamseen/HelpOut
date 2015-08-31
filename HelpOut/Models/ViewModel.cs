using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOut.Models
{
    public class ViewModel
    {
    }
    public class RoleUserVM
    {
        public IdentityRole Role { get; set; }
        public ICollection<ApplicationUser> Users { get; set; }
    }

}