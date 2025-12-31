using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class OrgUserDTO
    {
        public int OrgUserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailId { get; set; }
        public string Password { get; set; }
        public int OrgUserStatusId { get; set; }
        public string PhoneNo { get; set; }
        public int OrgId { get; set; }
        public int OrgUserTypeId { get; set; }
        public string BioData { get; set; }
        public string SearchTask { get; set; }
        public string UserName { get; set; }
    }
}
