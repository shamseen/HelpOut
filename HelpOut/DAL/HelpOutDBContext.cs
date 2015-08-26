using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpOut.Models;
using System.Data.Entity;


namespace HelpOut.DAL
{
    public class HelpOutDBContext : DbContext
    {
        public HelpOutDBContext() : base("DefaultConnection") { }
        //public DbSet<User> Users { get; set; }
        //public DbSet<Event> Events { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Event>()
                .HasMany(c => c.Attendees).WithMany(i => i.EventsAttending)
                .Map(t => t.MapLeftKey("EventID")
                    .MapRightKey("UserID")
                    .ToTable("Signups"));

        }
    }
}
