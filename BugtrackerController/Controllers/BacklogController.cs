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
    public class BacklogController : ApiController
    {
        private BacklogInterface backlogInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        BacklogController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            backlogInterface = new BacklogBusiness(loggedInUserDTO);
        }
        
        //Backlog
        [HttpGet]
        [Authorize]
        public List<ProductBacklogDTO> GetAllProductBacklog(int PhaseId)
        {
            try
            {
                return backlogInterface.GetAllProductBacklog(PhaseId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public ProductBacklogDTO GetProductBacklog(int ProductBacklogId)
        {
            try
            {
                return backlogInterface.GetProductBacklog(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardProductBacklogStateDTO GetAllProductBacklogState(int ProjectId, int PhaseId, int OrgUserId)
        {
            try
            {
                return backlogInterface.GetAllProductBacklogState(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public int CreateProductBacklog(ProductBacklogDTO Model)
        {
            try
            {
                return backlogInterface.CreateProductBacklog(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditProductBacklog(ProductBacklogDTO Model)
        {
            try
            {
                backlogInterface.EditProductBacklog(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteProductBacklog(int ProductBacklogId)
        {
            try
            {
                backlogInterface.DeleteProductBacklog(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<ProductBacklogDTO> SearchProductBacklog(ProductBacklogDTO Model)
        {
            try
            {
                return backlogInterface.SearchProductBacklog(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Backlog File
        [HttpGet]
        [Authorize]
        public void DeleteProductBacklogFile(int ProductBacklogFileId)
        {
            try
            {
                backlogInterface.DeleteProductBacklogFile(ProductBacklogFileId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Backlog User
        [HttpGet]
        [Authorize]
        public List<ProductBacklogDTO> GetAllProductBacklogUserWise(int PhaseId, int OrgUserId)
        {
            try
            {
                return backlogInterface.GetAllProductBacklogUserWise(PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Backlog MoveTo
        [HttpGet]
        [Authorize]
        public void PhaseMoveTo(int ProductBacklogId, int PhaseId)
        {
            try
            {
                backlogInterface.PhaseMoveTo(ProductBacklogId, PhaseId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
