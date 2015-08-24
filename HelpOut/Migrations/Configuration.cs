namespace HelpOut.Migrations
{
    using HelpOut.Models;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<HelpOut.DAL.HelpOutContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }


        protected override void Seed(HelpOut.DAL.HelpOutContext context)
        {
            var users = new List<User>
            {
                new User {Email = "Carson@aol.com",
                          Password = "Password",
                          FullName = "Carson Alexander", 
                          Location = "1234 Grand Circus Dr., Detroit, MI, 48197",
                          PhoneNumber = "7347563304", 
                          Description = "I like long walks on the beach ",
                          Website = "",},
                new User {Email = "Alonso@aol.com",
                    Password = "Password",
                    FullName = "Meredith Alsonso", 
                    Location = "902 Water Front ct., Westland MI, 48186",
                    PhoneNumber = "7347563304", 
                    Description = "",
                    Website = "",},
                 new User {Email = "Robertson@aol.com",
                     Password = "Password",
                     FullName = "Ashley Robinson",
                     Location = "12 Chirrewa Lane, Detroit, MI, 48197",
                     PhoneNumber = "8103390948", 
                     Description = "",
                     Website = "",},        
    };
            users.ForEach(s => context.Users.AddOrUpdate(p => p.FullName, s));
            context.SaveChanges();

        }
    }
}