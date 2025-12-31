using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface UserInterface
    {
        List<OrgUserDTO> GetAllUser();
        void CreateUser(OrgUserDTO Model);
        void EditUser(OrgUserDTO Model);
        void EditOrgUser(OrgUserDTO Model);
        List<OrgUserDTO> SearchUser(OrgUserDTO Model);

        void ChangeUserStatus(int StatusId, int OrgUserId);
        void ChangePassword(string NewPassword, string OldPassword);
        
        List<MapuserDTO> GetAllMapUser(int ProjectId);
        void MapuserToProject(MapUserBM Model);
        void RemoveuserFromProject(MapUserBM Model);
    }
}
