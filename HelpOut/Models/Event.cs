using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpOut.Models
{
    public class Event
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        //foreign key for our User w/ role organization that made the event
        public int OrganizationID { get; set; }


        //navigation property
        [ForeignKey("OrganizationID")] //workaround for the foreign key mapping issue
        public User Organization { get; set; }
        public virtual ICollection<User> Attendees { get; set; }
    }
}

