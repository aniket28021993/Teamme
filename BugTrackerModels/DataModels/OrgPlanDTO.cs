using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class OrgPlanDTO
    {
        public int UpdateOrgPlanId { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public int OrgPlanId { get; set; }
        public bool IsApproved { get; set; }
    }
}
