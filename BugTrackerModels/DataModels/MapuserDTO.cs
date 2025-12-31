using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class MapuserDTO
    {
        public int UserProjectMapId { get; set; }
        public int UserId { get; set; }
        public int ProjectId { get; set; }
        public int OrgId { get; set; }
        public string UserName { get; set; }
        public int UserTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public int OrgUserStatusId { get; set; }
        public int LoggedInId { get; set; }
        public DateTime LogoutDate { get; set; }
        public int LoggedOutId { get; set; }
        public string PhoneNo { get; set; }
        public string EmailId { get; set; }
        public string ProfileImage { get; set; }
        public int BugCount { get; set; }
        public int EnhancementCount { get; set; }
        public int TaskCount { get; set; }
    }
}
