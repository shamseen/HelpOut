using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HelpOut.Models
{
    public class User
    {
        //primary key
        public int UserID { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
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
    }
}
