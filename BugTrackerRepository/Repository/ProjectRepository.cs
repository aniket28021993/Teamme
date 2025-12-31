using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BugTrackerRepository.Repository
{
    public class ProjectRepository
    {
        public List<ProjectDTO> GetAllProject(SqlConnection con, int OrgId)
        {
            List<ProjectDTO> projectList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    projectList = new List<ProjectDTO>();
                    while (dr.Read())
                    {
                        ProjectDTO projectDTO = new ProjectDTO();
                        projectDTO._ProjectId = Convert.ToInt32(dr["ProjectId"]);
                        projectDTO._ProjectName = dr["Description"].ToString();
                        projectList.Add(projectDTO);
                    }
                }
                return projectList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProjectDTO> GetAllProjectUserWise(SqlConnection con, int OrgId, int OrgUserId)
        {
            List<ProjectDTO> projectList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProjectUserWise", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    projectList = new List<ProjectDTO>();
                    while (dr.Read())
                    {
                        ProjectDTO projectDTO = new ProjectDTO();
                        projectDTO._ProjectId = Convert.ToInt32(dr["ProjectId"]);
                        projectDTO._ProjectName = dr["Description"].ToString();
                        projectList.Add(projectDTO);
                    }
                }
                return projectList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void CreateProject(SqlConnection con, int OrgId, int OrgUserId, string Description)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@Description", Description));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditProject(SqlConnection con, ProjectDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Description", Model._ProjectName));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model._ProjectId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeleteProject(SqlConnection con, int ProjectId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProjectDTO> SearchProject(SqlConnection con, ProjectDTO Model)
        {
            List<ProjectDTO> projectList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    projectList = new List<ProjectDTO>();
                    while (dr.Read())
                    {
                        ProjectDTO projectDTO = new ProjectDTO();
                        projectDTO._ProjectId = Convert.ToInt32(dr["ProjectId"]);
                        projectDTO._ProjectName = dr["Description"].ToString();
                        projectList.Add(projectDTO);
                    }
                }
                return projectList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
