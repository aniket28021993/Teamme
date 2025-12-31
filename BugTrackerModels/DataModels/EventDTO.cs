using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class EventDTO
    {
        public int EventId { get; set; }
        public string Title { get; set; }
        public List<int> Attendee { get; set; }
        public DateTime EventDate { get; set; }
        public string EventMOM { get; set; }
        public int CreatedBy { get; set; }
        public int ProjectId { get; set; }
        public int OrgId { get; set; }
        public List<EventUserDTO> EventUser { get; set; }
    }

    public class EventUserDTO
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int EventId { get; set; }
    }
}
