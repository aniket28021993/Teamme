using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class ProjectDTO
    {
        public int _ProjectId { get; set; }
        public string _ProjectName { get; set; }
        public int OrgId { get; set; }
        public string SearchTask { get; set; }
    }
}
