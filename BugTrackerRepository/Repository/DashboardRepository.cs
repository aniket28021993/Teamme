using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerRepository.Repository
{
    public class DashboardRepository
    {       

        #region FILE
        public void FileUpload(SqlConnection con, int ProductBacklogId, String FileName, string RefFileName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_FileUpload", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@FileName", FileName));
                cmd.Parameters.Add(new SqlParameter("@RefFileName", RefFileName));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void FileUploadData(SqlConnection con, int ProductBacklogDataId, String FileName, string RefFileName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_FileUploadData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", ProductBacklogDataId));
                cmd.Parameters.Add(new SqlParameter("@FileName", FileName));
                cmd.Parameters.Add(new SqlParameter("@RefFileName", RefFileName));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void VideoUpload(SqlConnection con, int ProductBacklogId, String VideoName, string RefVideoName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_VideoUpload", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@VideoName", VideoName));
                cmd.Parameters.Add(new SqlParameter("@RefVideoName", RefVideoName));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void VideoUploadData(SqlConnection con, int ProductBacklogDataId, String VideoName, string RefVideoName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_VideoUploadData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", ProductBacklogDataId));
                cmd.Parameters.Add(new SqlParameter("@VideoName", VideoName));
                cmd.Parameters.Add(new SqlParameter("@RefVideoName", RefVideoName));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ImageUpload(SqlConnection con, int OrgUserId, String ImageName)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_ImageUpload", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@ImageName", ImageName));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region DASHBOARD

        public List<ProductBacklogDataDTO> GetAllUserDashboardProductTask(SqlConnection con, int OrgId, int PhaseId, int OrgUserId)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllUserDashboardProductTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    productBacklogDataList = new List<ProductBacklogDataDTO>();
                    while (dr.Read())
                    {
                        ProductBacklogDataDTO productBacklogDataDTO = new ProductBacklogDataDTO();
                        productBacklogDataDTO.ProductBacklogDataId = Convert.ToInt32(dr["ProductBacklogDataId"]);
                        productBacklogDataDTO.Title = dr["Title"].ToString();
                        productBacklogDataDTO.Description = dr["Description"].ToString();
                        productBacklogDataDTO.State = Convert.ToInt32(dr["State"]);
                        productBacklogDataDTO.AssignedTo = Convert.ToInt32(dr["AssignedTo"]);
                        productBacklogDataDTO.ProductBacklogTypeId = Convert.ToInt32(dr["ProductBacklogTypeId"]);
                        productBacklogDataDTO.UserName = dr["UserName"].ToString();
                        productBacklogDataDTO.StateName = dr["StateName"].ToString();
                        productBacklogDataDTO.TypeDescription = dr["TypeDescription"].ToString();
                        productBacklogDataDTO.FileName = dr["FileName"].ToString();
                        productBacklogDataDTO.RefFileName = dr["RefFileName"].ToString();
                        productBacklogDataDTO.VideoName = dr["VideoName"].ToString();
                        productBacklogDataDTO.RefVideoName = dr["RefVideoName"].ToString();
                        productBacklogDataDTO.Estimation = Convert.ToInt32(dr["Estimation"]);
                        productBacklogDataDTO.UsedEstimation = Convert.ToInt32(dr["UsedEstimation"]);
                        productBacklogDataDTO.RemainingEstimation = Convert.ToInt32(dr["RemainingEstimation"]);
                        productBacklogDataDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        productBacklogDataList.Add(productBacklogDataDTO);
                    }
                }
                return productBacklogDataList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DashboardCountDTO GetAllDashboardCount(SqlConnection con, int OrgId, int ProjectId,int PhaseId,int OrgUserId)
        {
            DashboardCountDTO dashboardCountDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dashboardCountDTO = new DashboardCountDTO();
                        dashboardCountDTO.ProductBacklogCount = Convert.ToInt32(dr["ProductBacklogCount"]);
                        dashboardCountDTO.TaskCount = Convert.ToInt32(dr["TaskCount"]);
                        dashboardCountDTO.EnhancementCount = Convert.ToInt32(dr["EnhancementCount"]);
                        dashboardCountDTO.BugCount = Convert.ToInt32(dr["BugCount"]);
                        dashboardCountDTO.PhaseCount = Convert.ToInt32(dr["PhaseCount"]);
                        dashboardCountDTO.TestCaseCount = Convert.ToInt32(dr["TestCaseCount"]);
                    }
                }
                con.Close();
                return dashboardCountDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<MapuserDTO> GetAllDashboardUser(SqlConnection con, int ProjectId, int OrgId)
        {
            List<MapuserDTO> mapuserList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    mapuserList = new List<MapuserDTO>();
                    while (dr.Read())
                    {
                        MapuserDTO mapuserDTO = new MapuserDTO();
                        mapuserDTO.UserId = Convert.ToInt32(dr["UserId"]);
                        mapuserDTO.OrgId = Convert.ToInt32(dr["OrgId"]);
                        mapuserDTO.UserName = dr["UserName"].ToString();
                        mapuserDTO.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
                        mapuserDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        mapuserDTO.OrgUserStatusId = Convert.ToInt32(dr["OrgUserStatusId"]);
                        mapuserDTO.LoggedInId = Convert.ToInt32(dr["LoggedInId"]);
                        mapuserDTO.LoggedOutId = Convert.ToInt32(dr["LoggedOutId"]);
                        mapuserDTO.LogoutDate = Convert.ToDateTime(dr["LogoutDate"]);
                        mapuserDTO.EmailId = dr["EmailId"].ToString();
                        mapuserDTO.PhoneNo = dr["PhoneNo"].ToString();
                        mapuserDTO.ProfileImage = dr["ProfileImage"].ToString();
                        mapuserList.Add(mapuserDTO);
                    }
                }
                con.Close();
                return mapuserList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void GetAllDashboardUserRemainingCount(SqlConnection con, MapuserDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardUserRemainingCount", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", Model.UserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        Model.BugCount = Convert.ToInt32(dr["BugCount"]);
                        Model.EnhancementCount = Convert.ToInt32(dr["EnhancementCount"]);
                        Model.TaskCount = Convert.ToInt32(dr["TaskCount"]);
                    }
                }
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DashboardProductBacklogStatus GetAllDashboardProductBacklogGraph(SqlConnection con, DashboardProductBacklogStatus model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardProductBacklogGraph", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", model.ProductBacklogId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        model.TaskCount = Convert.ToInt32(dr["TaskCount"]);
                        model.EnhancementCount = Convert.ToInt32(dr["EnhancementCount"]);
                        model.BugCount = Convert.ToInt32(dr["BugCount"]);
                        model.ProductBacklogName = dr["ProductBacklogName"].ToString();
                    }
                }
                con.Close();
                return model;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public DashboardPriorityBugDTO GetAllProductBugPriorityWise(SqlConnection con, int OrgId, int ProjectId, int PhaseId,int OrgUserId)
        {
            DashboardPriorityBugDTO priorityBugDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllPriorityBug", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        priorityBugDTO = new DashboardPriorityBugDTO();
                        priorityBugDTO.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                        priorityBugDTO.P1Count = Convert.ToInt32(dr["P1Count"]);
                        priorityBugDTO.P2Count = Convert.ToInt32(dr["P2Count"]);
                        priorityBugDTO.P3Count = Convert.ToInt32(dr["P3Count"]);
                        priorityBugDTO.P4Count = Convert.ToInt32(dr["P4Count"]);
                    }
                }
                con.Close();
                return priorityBugDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<ProductBacklogDTO> GetAllDashboardProductBacklog(SqlConnection con, int OrgId, int PhaseId,int OrgUserId)
        {
            List<ProductBacklogDTO> productBacklogList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    productBacklogList = new List<ProductBacklogDTO>();
                    while (dr.Read())
                    {
                        ProductBacklogDTO productBacklogDTO = new ProductBacklogDTO();
                        productBacklogDTO.ProductBacklogId = Convert.ToInt32(dr["ProductBacklogId"]);
                        productBacklogDTO.Title = dr["Title"].ToString();
                        productBacklogDTO.Description = dr["Description"].ToString();
                        productBacklogDTO.State = Convert.ToInt32(dr["State"]);
                        productBacklogDTO.AssignedTo = Convert.ToInt32(dr["AssignedTo"]);
                        productBacklogDTO.UserName = dr["UserName"].ToString();
                        productBacklogDTO.StateName = dr["StateName"].ToString();
                        productBacklogDTO.FileName = dr["FileName"].ToString();
                        productBacklogDTO.RefFileName = dr["RefFileName"].ToString();
                        productBacklogDTO.VideoName = dr["VideoName"].ToString();
                        productBacklogDTO.RefVideoName = dr["RefVideoName"].ToString();
                        productBacklogDTO.Estimation = Convert.ToInt32(dr["Estimation"]);
                        productBacklogDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        productBacklogList.Add(productBacklogDTO);
                    }
                }
                return productBacklogList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

       
    }
}
