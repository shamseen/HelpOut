using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HelpOut.Models;

namespace HelpOut.Domain
{
    public class UpcomingEventFinder
    {
        private List<EventDTO> _upcoming = new List<Event>();

        public UpcomingEventFinder(List<Event> upcoming)
        {
            _upcoming = upcoming;
        }

        public List<Event> FindUpcomingEvents(DateTime currentTime)
        {
            var 
        }



    }
}
