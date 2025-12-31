using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class TaskController : ApiController
    {
        private TaskInterface taskInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        TaskController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            taskInterface = new TaskBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<ProductBacklogDataDTO> GetAllProductTask(int ProductBacklogId)
        {
            try
            {
                return taskInterface.GetAllProductTask(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<ProductBacklogDataDTO> GetAllDashboardProductTask(int PhaseId, int OrgUserId)
        {
            try
            {
                return taskInterface.GetAllDashboardProductTask(PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardProductTaskStateDTO GetAllProductTaskState(int ProjectId, int PhaseId, int OrgUserId)
        {
            try
            {
                return taskInterface.GetAllProductTaskState(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public int CreateTask(ProductBacklogDataDTO Model)
        {
            try
            {
                return taskInterface.CreateTask(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditTask(ProductBacklogDataDTO Model)
        {
            try
            {
                taskInterface.EditTask(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteTask(int TaskId)
        {
            try
            {
                taskInterface.DeleteTask(TaskId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void TaskToEnhancement(int TaskId)
        {
            try
            {
                taskInterface.TaskToEnhancement(TaskId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<ProductBacklogDataDTO> SearchTask(ProductBacklogDataDTO Model)
        {
            try
            {
                return taskInterface.SearchTask(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
