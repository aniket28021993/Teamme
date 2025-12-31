using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class RecentActivityDTO
    {
        public string PreviousDescription { get; set; }
        public string NewDescription { get; set; }
        public string ColumnName { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedOn { get; set; }
        public int RecentActivityTypeId { get; set; }
        public int CommonId { get; set; }
    }

    public class RecentActivityDateDTO
    {
        public DateTime CreatedOn { get; set; }
        public List<RecentActivityDTO> recentActivityDTO { get; set; }
    }
}
