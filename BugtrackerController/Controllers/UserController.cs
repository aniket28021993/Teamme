using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class UserController : ApiController
    {
        private UserInterface userInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        UserController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            userInterface = new UserBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<OrgUserDTO> GetAllUser()
        {
            try
            {
                return userInterface.GetAllUser();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreateUser(OrgUserDTO Model)
        {
            try
            {
                userInterface.CreateUser(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditUser(OrgUserDTO Model)
        {
            try
            {
                userInterface.EditUser(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditOrgUser(OrgUserDTO Model)
        {
            try
            {
                userInterface.EditOrgUser(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<OrgUserDTO> SearchUser(OrgUserDTO Model)
        {
            try
            {
                return userInterface.SearchUser(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void ChangeUserStatus(int StatusId, int OrgUserId)
        {
            try
            {
                userInterface.ChangeUserStatus(StatusId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void ChangePassword(string NewPassword, string OldPassword)
        {
            try
            {
                userInterface.ChangePassword(NewPassword, OldPassword);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<MapuserDTO> GetAllMapUser(int ProjectId)
        {
            try
            {
                return userInterface.GetAllMapUser(ProjectId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void MapuserToProject(MapUserBM Model)
        {
            try
            {
                userInterface.MapuserToProject(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void RemoveuserFromProject(MapUserBM Model)
        {
            try
            {
                userInterface.RemoveuserFromProject(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        
    }
}
