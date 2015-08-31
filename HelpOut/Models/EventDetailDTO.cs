using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOut.Models
{
    public class EventDetailDTO
    {
        public int EventID { get; set; }
        public string Name { get; set; }
        public DateTime DateTime { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }
        public string OrganizationName { get; set; }
        public string OrganizationID { get; internal set; }
    }
}