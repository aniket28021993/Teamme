using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class LoggedInUserDTO
    {
        public int _OrgUserId { get; set; }
        public string _FirstName { get; set; }
        public String _LastName { get; set; }
        public int _OrgUserStatusId { get; set; }
        public string _PhoneNo { get; set; }
        public int _OrgId { get; set; }
        public string _OrgName { get; set; }
        public int _OrgStatusId { get; set; }
        public string _EmailId { get; set; }
        public int _OrgUserTypeId { get; set; }
        public string _ProfileImage { get; set; }
        public string _BioData { get; set; }
        public int _OrgPlanId { get; set; }

        public string _OrgErrMsg { get; set; }
    }
}
