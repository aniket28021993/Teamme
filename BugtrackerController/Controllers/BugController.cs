using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class BugController : ApiController
    {
        private BugInterface bugInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        BugController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            bugInterface = new BugBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<ProductBacklogDataDTO> GetAllProductBug(int ProductBacklogId)
        {
            try
            {
                return bugInterface.GetAllProductBug(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardProductBugStateDTO GetAllProductBugState(int ProjectId, int PhaseId, int OrgUserId)
        {
            try
            {
                return bugInterface.GetAllProductBugState(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public int CreateBug(ProductBacklogDataDTO Model)
        {
            try
            {
                return bugInterface.CreateBug(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditBug(ProductBacklogDataDTO Model)
        {
            try
            {
                bugInterface.EditBug(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteBug(int BugId)
        {
            try
            {
                bugInterface.DeleteBug(BugId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void BugToEnhancement(int BugId)
        {
            try
            {
                bugInterface.BugToEnhancement(BugId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<ProductBacklogDataDTO> SearchBug(ProductBacklogDataDTO Model)
        {
            try
            {
                return bugInterface.SearchBug(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
