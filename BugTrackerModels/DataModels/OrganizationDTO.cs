using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class OrganizationDTO
    {
        public int _OrgId { get; set; }
        public string _OrgName { get; set; }
        public string _OrgEmail { get; set; }
        public string _OrgNumber { get; set; }
        public string _OrgAddress { get; set; }
        public int _OrgStatusId { get; set; }
        public int _OrgPlanId { get; set; }
    }
}
