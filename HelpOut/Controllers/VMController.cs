using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using HelpOut.Models;
using System.Collections.Generic;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Net;

namespace HelpOut.Controllers
{
    public class VMController : Controller
    {

        // GET: ApplicationUsers
        public ActionResult Index()
        {

            using (var context = new ApplicationDbContext())
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);
                var roleID = manager.FindByName("Organization").Id;
                var Organizations = context.Users.Where(u => u.Roles.Any(r => r.RoleId == roleID)).ToList();//List of organizations
                return this.View(Organizations);
            }

        }
    }
}