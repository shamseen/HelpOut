using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using RazorPDF;

namespace HelpOut.Models
{
    public class AttendanceRoster
    {
        public int AttendanceRosterID { get; set; }
        public string Name { get; set; }
        public virtual ICollection<ApplicationUser> Attendees { get; set; }
        public bool? Present { get; set; }
    }
}