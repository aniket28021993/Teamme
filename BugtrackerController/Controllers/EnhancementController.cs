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
    public class EnhancementController : ApiController
    {
        private EnhancementInterface enhancementInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        EnhancementController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            enhancementInterface = new EnhancementBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<ProductBacklogDataDTO> GetAllProductEnhancement(int ProductBacklogId)
        {
            try
            {
                return enhancementInterface.GetAllProductEnhancement(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardProductEnhancementStateDTO GetAllProductEnhancementState(int ProjectId, int PhaseId, int OrgUserId)
        {
            try
            {
                return enhancementInterface.GetAllProductEnhancementState(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public int CreateEnhancement(ProductBacklogDataDTO Model)
        {
            try
            {
              return enhancementInterface.CreateEnhancement(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditEnhancement(ProductBacklogDataDTO Model)
        {
            try
            {
                enhancementInterface.EditEnhancement(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteEnhancement(int EnhancementId)
        {
            try
            {
                enhancementInterface.DeleteEnhancement(EnhancementId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void EnhancementToTaskBug(int EnhancementId,int TypeId)
        {
            try
            {
                enhancementInterface.EnhancementToTaskBug(EnhancementId,TypeId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<ProductBacklogDataDTO> SearchEnhancement(ProductBacklogDataDTO Model)
        {
            try
            {
                return enhancementInterface.SearchEnhancement(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
