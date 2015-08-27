using HelpOut.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HelpOut.ModelViews
{
    public class UserProfileDTO
    {
        public string UserId { get; set; }
        public string Name { get; set; }
        public string image { get; set; }
        public ICollection<Event> myevent { get; set; }
    }
}