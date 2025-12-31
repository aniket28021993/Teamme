using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.BusinessModels
{
    public class MapUserBM
    {
        public int ProjectId { get; set; }
        public List<SelectedList> Users { get; set; }
    }

    public class ProductBacklogList
    {
        public List<SelectedList> Backlog { get; set; }
    }

    public class SelectedList
    {
        public int id { get; set; }
        public bool text { get; set; }
    }
}
