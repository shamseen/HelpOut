using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace HelpOut.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {

        //public int ApplicationUserID { get; set;}
        public string FullName { get; set; }
        public string Location { get; set; }

        [Required(ErrorMessage = "Your must provide a PhoneNumber")]
        [Display(Name = "PhoneNumber")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        //another regular expression: @"^(?:\d{8}|00\d{10}|\+\d{2}\d{8})$"
        public string PhoneNumber { get; set; }
        public string Description { get; set; }
        public string Website { get; set; }

        //navigation properties
        public ICollection<Event> EventsAttending { get; set; } //for volunteers
        public virtual ICollection<Event> EventsCreated { get; set; } //for organizations

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
        
        public DbSet<Event> Events { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}