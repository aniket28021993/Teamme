using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class ProjectController : ApiController
    {
        private ProjectInterface projectInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        ProjectController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            projectInterface = new ProjectBusiness(loggedInUserDTO);
        }

        [HttpGet]
        [Authorize]
        public List<ProjectDTO> GetAllProject()
        {
            try
            {
                return projectInterface.GetAllProject();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void CreateProject(string Description)
        {
            try
            {
                projectInterface.CreateProject(Description);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditProject(ProjectDTO Model)
        {
            try
            {
                projectInterface.EditProject(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteProject(int ProjectId)
        {
            try
            {
                projectInterface.DeleteProject(ProjectId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<ProjectDTO> SearchProject(ProjectDTO Model)
        {
            try
            {
                return projectInterface.SearchProject(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
