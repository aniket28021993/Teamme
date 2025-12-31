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
    public class GenericController : ApiController
    {
        private GenericInterface genericInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        GenericController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            genericInterface = new GenericBusiness(loggedInUserDTO);
        }

        #region COMMENT
        [HttpGet]
        [Authorize]
        public List<CommentDTO> GetAllComment(int CommonId, int CommentTypeId)
        {
            try
            {
                return genericInterface.GetAllComment(CommonId, CommentTypeId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreateComment(CommentDTO Model)
        {
            try
            {
                genericInterface.CreateComment(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region RECENT ACTIVITY

        [HttpGet]
        [Authorize]
        public List<RecentActivityDateDTO> GetAllRecentActivity(int CommonId, int RecentActivityTypeId)
        {
            try
            {
                return genericInterface.GetAllRecentActivity(CommonId, RecentActivityTypeId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region ORG PLAN

        [HttpGet]
        [Authorize]
        public void UpdateOrgPlan(int OrgPlanId)
        {
            try
            {
                genericInterface.UpdateOrgPlan(OrgPlanId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region COMMON
        [HttpGet]
        [Authorize]
        public ProductBacklogDataDTO GetProductBacklogData(int ProductBacklogDataId)
        {
            try
            {
                return genericInterface.GetProductBacklogData(ProductBacklogDataId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteProductBacklogDataFile(int ProductBacklogDataFileId)
        {
            try
            {
                genericInterface.DeleteProductBacklogDataFile(ProductBacklogDataFileId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
