namespace HelpOut.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpOut.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(HelpOut.Models.ApplicationDbContext context)
        {
            //seeding roles
            if (!context.Roles.Any(r => r.Name == "Organization"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new IdentityRole() { Name = "Organization" };
                manager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Volunteer"))
            {
                var store = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(store);

                var role = new IdentityRole() { Name = "Volunteer" };
                manager.Create(role);
            }
            //seeding users - 1 organization, 2 volunteers  ==> creates ApplicationUser_Id1 in UserSeeds migration?????
            if (!context.Users.Any(u => u.Email == "h4h@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Email = "h4h@gmail.com",
                    UserName = "BobsTheBuilders",
                    FullName = "Habitat for Humanity",
                    Location = "270 Peachtree Street NW, Atlanta, GA 30303",
                    Description = "Habitat for Humanity’s vision is a world where everyone has a decent place to live. Our mission is to put God’s love into action by bringing people together to build homes, communities and hope.",
                    Website = "http://www.habitat.org/"
                };

                manager.Create(user, "Password1!");
                manager.AddToRole(user.Id, "Organization");
            }

            if (!context.Users.Any(u => u.Email == "joewash@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Email = "joewash@gmail.com",
                    UserName = "JoeShmoe",
                    FullName = "George Washington",
                    Location = "101 Callahan Dr, Alexandria, VA 22301",
                    Description = "The first President of the United States(1789–1797), the commander in chief of the Continental Army during the American Revolutionary War, and one of the Founding Fathers of the United States.",
                    Website = "https://simple.wikipedia.org/wiki/George_Washington"
                };

                manager.Create(user, "Password1!");
                manager.AddToRole(user.Id, "Volunteer");
            }

            if (!context.Users.Any(u => u.Email == "kenjohnson@gmail.com"))
            {
                var store = new UserStore<ApplicationUser>(context);
                var manager = new UserManager<ApplicationUser>(store);
                var user = new ApplicationUser()
                {
                    Email = "kenjohnson@gmail.com",
                    UserName = "Airport",
                    FullName = "John F. Kennedy",
                    Location = "646 Main St, Dallas, TX 75202",
                    Description = "Commonly known as 'Jack' or by his initials JFK, was the 35th President of the United States. He was in office from 1961-1963. He was the youngest President elected to the office, at the age of 43.",
                    Website = "https://simple.wikipedia.org/wiki/John_F._Kennedy"
                };

                manager.Create(user, "Password1!");
                manager.AddToRole(user.Id, "Volunteer");
            }
        }
    }
}