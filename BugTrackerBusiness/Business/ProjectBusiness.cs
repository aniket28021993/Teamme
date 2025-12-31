using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.Business
{
    public class ProjectBusiness: ProjectInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public ProjectBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        public List<ProjectDTO> GetAllProject()
        {
            List<ProjectDTO> projectDTOs = null;
            ProjectRepository projectRepository = new ProjectRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (this.loggedInUserDTO._OrgUserTypeId == 2)
                    {
                        projectDTOs = projectRepository.GetAllProject(con, this.loggedInUserDTO._OrgId);
                    }
                    else
                    {
                        projectDTOs = projectRepository.GetAllProjectUserWise(con, this.loggedInUserDTO._OrgId, this.loggedInUserDTO._OrgUserId);
                    }

                    return projectDTOs;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void CreateProject(string Description)
        {
            ProjectRepository projectRepository = new ProjectRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                try
                {
                    con.Open();
                    projectRepository.CreateProject(con, this.loggedInUserDTO._OrgId, this.loggedInUserDTO._OrgUserId, Description);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void EditProject(ProjectDTO Model)
        {
            ProjectRepository projectRepository = new ProjectRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                try
                {
                    con.Open();
                    projectRepository.EditProject(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void DeleteProject(int ProjectId)
        {
            ProjectRepository projectRepository = new ProjectRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    projectRepository.DeleteProject(con, ProjectId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public List<ProjectDTO> SearchProject(ProjectDTO Model)
        {
            ProjectRepository projectRepository = new ProjectRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return projectRepository.SearchProject(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
    }
}
