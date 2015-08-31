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
            var roleStore = new RoleStore<IdentityRole>(context);
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            if (!context.Roles.Any(r => r.Name == "Organization"))
            {
                var role = new IdentityRole() { Name = "Organization" };
                roleManager.Create(role);
            }

            if (!context.Roles.Any(r => r.Name == "Volunteer"))
            {
                var role = new IdentityRole() { Name = "Volunteer" };
                roleManager.Create(role);
            }

            context.SaveChanges();
            roleStore.Dispose();
            roleManager.Dispose();



            //seeding users
            var userStore = new UserStore<ApplicationUser>(context);
            var userManager = new UserManager<ApplicationUser>(userStore);
            var passwordHash = new PasswordHasher();

            //habitat for humanity
            var user = new ApplicationUser()
            {
                Email = "h4h@gmail.com",
                UserName = "BobsTheBuilders",
                FullName = "Habitat for Humanity",
                Location = "270 Peachtree Street NW, Atlanta, GA 30303",
                Description = "Habitat for Humanity’s vision is a world where everyone has a decent place to live. Our mission is to put God’s love into action by bringing people together to build homes, communities and hope.",
                Website = "http://www.habitat.org/",
                PasswordHash = passwordHash.HashPassword("Password1!"),
                EventsCreated = new List<Event>()
            };

            if (!context.Users.Any(u => u.Email == "h4h@gmail.com"))
            {
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Organization");
            }
            else
                userManager.Update(user);


            //red cross
            user = new ApplicationUser()
            {
                Email = "redcross@gmail.com",
                UserName = "Switzerland",
                FullName = "American Red Cross",
                Location = "2025 E Street, NW, Washington DC 20006",
                Description = "The American Red Cross exists to provide compassionate care to those in need. Our network of generous donors, volunteers and employees share a mission of preventing and relieving suffering, here at home and around the world.",
                Website = "http://www.redcross.org/",
                PasswordHash = passwordHash.HashPassword("Password1!"),
                EventsCreated = new List<Event>()
            };

            if (!context.Users.Any(u => u.Email == "redcross@gmail.com"))
            {
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Organization");
            }
            else
                userManager.Update(user);



            //george washington
            user = new ApplicationUser()
            {
                Email = "joewash@gmail.com",
                UserName = "JoeShmoe",
                FullName = "George Washington",
                Location = "101 Callahan Dr, Alexandria, VA 22301",
                Description = "The first President of the United States(1789–1797), the commander in chief of the Continental Army during the American Revolutionary War, and one of the Founding Fathers of the United States.",
                Website = "https://simple.wikipedia.org/wiki/George_Washington",
                PasswordHash = passwordHash.HashPassword("Password1!"),
                EventsAttending = new List<Event>()
            };

            if (!context.Users.Any(u => u.Email == "joewash@gmail.com"))
            {
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Volunteer");
            }
            else
                userManager.Update(user);



            //john f kennedy
            user = new ApplicationUser()
            {
                Email = "kenjohnson@gmail.com",
                UserName = "Airport",
                FullName = "John F. Kennedy",
                Location = "646 Main St, Dallas, TX 75202",
                Description = "Commonly known as 'Jack' or by his initials JFK, was the 35th President of the United States. He was in office from 1961-1963. He was the youngest President elected to the office, at the age of 43.",
                Website = "https://simple.wikipedia.org/wiki/John_F._Kennedy",
                PasswordHash = passwordHash.HashPassword("Password1!"),
                EventsAttending = new List<Event>()
            };

            if (!context.Users.Any(u => u.Email == "kenjohnson@gmail.com"))
            {
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Volunteer");
            }
            else
                userManager.Update(user);



            //martin luther king, jr.
            user = new ApplicationUser()
            {
                Email = "mlk@gmail.com",
                UserName = "Milk",
                FullName = "Martin Luther King, Jr.",
                Location = "Independence Avenue Southwest, Washington, DC 20024",
                Description = "American pastor, activist, humanitarian, and leader in the African-American Civil Rights Movement. He is best known for his role in the advancement of civil rights using nonviolent civil disobedience based on his Christian beliefs.",
                Website = "https://simple.wikipedia.org/wiki/Martin_Luther_King,_Jr.",
                PasswordHash = passwordHash.HashPassword("Password1!"),
                EventsAttending = new List<Event>()
            };

            if (!context.Users.Any(u => u.Email == "mlk@gmail.com"))
            {
                userManager.Create(user);
                userManager.AddToRole(user.Id, "Volunteer");
            }
            else
                userManager.Update(user);


            userStore.Dispose();
            userManager.Dispose();
            context.SaveChanges();

            //seeding events
            var events = new List<Event> {

                new Event
                {
                    EventID = 1,
                    Name = "First Ever Fundraiser to Build Seed!",
                    DateTime = DateTime.ParseExact("06/15/2015 13:45:00", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "Shamseen's house",
                    Description = "We're praying this will actually work and we can show users' events.",
                    //OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                    OrganizationID = (from u in context.Users
                                     where u.Email == "h4h@gmail.com"
                                     select u.Id).First().ToString()
                },

                new Event
                {
                    EventID = 2,
                    Name = "George-Only Party",
                    DateTime = DateTime.ParseExact("01/15/2020 09:15:00", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "White House",
                    Description = "Spreading the seed. JOHN IS NOT ALLOWED. JUST LEAVE, JOHN.",
                    //OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                    OrganizationID = (from u in context.Users
                                     where u.Email == "h4h@gmail.com"
                                     select u.Id).First().ToString()
                },

                new Event
                {
                    EventID = 3,
                    Name = "Kennedy Family Reunion",
                    DateTime = DateTime.ParseExact("11/11/2011 11:11:11", "MM/dd/yyyy HH:mm:ss", null),
                    Location = "The nearest bar",
                    Description = "Hopefully this will work.",
                    //OrganizationID = "67ef0428-03df-4e1e-88e2-f9a589fbbfcf", //habitat for humanity
                    OrganizationID = (from u in context.Users
                                     where u.Email == "redcross@gmail.com"
                                     select u.Id).First().ToString()
                }
                };

            //adding each event to actual database
            events.ForEach(ev => context.Events.AddOrUpdate(e => e.EventID, ev));
            context.SaveChanges();

        }
    }
}
