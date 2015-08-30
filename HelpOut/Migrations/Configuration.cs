namespace HelpOut.Migrations
{
    using Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using System.Collections.Generic;

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
                    Website = "http://www.habitat.org/",
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

            //seeding events
            var events = new List<Event> {

                new Event
                {
                    EventID = 1,
                    Name = "First Ever Fundraiser to Build Seed!",
                    DateTime = DateTime.ParseExact("06/15/2015 13:45:00", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "Shamseen's house",
                    Description = "We're praying this will actually work and we can show users' events.",
                    OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                },

                new Event
                {
                    EventID = 2,
                    Name = "George-Only Party",
                    DateTime = DateTime.ParseExact("01/15/2020 09:15:00", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "White House",
                    Description = "Spreading the seed. JOHN IS NOT ALLOWED. JUST LEAVE, JOHN.",
                    OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                },

                new Event
                {
                    EventID = 3,
                    Name = "Kennedy Family Reunion",
                    DateTime = DateTime.ParseExact("11/11/2011 11:11:11", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "The nearest bar",
                    Description = "Hopefully this will work.",
                    OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                }
                };

            events.ForEach(ev => context.Events.AddOrUpdate(e => e.EventID, ev));
            context.SaveChanges();
            
        }
    }
}


