using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class PhaseDTO
    {
        public int PhaseId { get; set; }
        public int ProjectId { get; set; }
        public int OrgId { get; set; }
        public string Description { get; set; }
        public string SearchTask { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}
