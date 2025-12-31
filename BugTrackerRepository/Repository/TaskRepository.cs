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
    public class TaskRepository
    {
        public List<ProductBacklogDataDTO> GetAllProductTask(SqlConnection con, int OrgId, int ProductBacklogId)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
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
                        productBacklogDataList.Add(productBacklogDataDTO);

                        productBacklogDataDTO.Estimation = Convert.ToInt32(dr["Estimation"]);
                        productBacklogDataDTO.UsedEstimation = Convert.ToInt32(dr["UsedEstimation"]);
                        productBacklogDataDTO.RemainingEstimation = Convert.ToInt32(dr["RemainingEstimation"]);
                    }
                }
                con.Close();
                return productBacklogDataList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDataDTO> GetAllDashboardProductTask(SqlConnection con, int OrgId, int PhaseId, int OrgUserId)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllDashboardProductTask", con);
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
                        productBacklogDataDTO.Title = dr["Title"].ToString();
                        productBacklogDataDTO.ProductBacklogDataId = Convert.ToInt32(dr["ProductBacklogDataId"]);
                        productBacklogDataDTO.Estimation = Convert.ToInt32(dr["Estimation"]);
                        productBacklogDataDTO.UsedEstimation = Convert.ToInt32(dr["UsedEstimation"]);
                        productBacklogDataDTO.RemainingEstimation = Convert.ToInt32(dr["RemainingEstimation"]);
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
        public DashboardProductTaskStateDTO GetAllProductTaskState(SqlConnection con, int OrgId, int ProjectId, int PhaseId, int OrgUserId)
        {
            DashboardProductTaskStateDTO dashboardProductTaskStateDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductTaskState", con);
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
                        dashboardProductTaskStateDTO = new DashboardProductTaskStateDTO();
                        dashboardProductTaskStateDTO.New = Convert.ToInt32(dr["New"]);
                        dashboardProductTaskStateDTO.Active = Convert.ToInt32(dr["Active"]);
                        dashboardProductTaskStateDTO.Paused = Convert.ToInt32(dr["Paused"]);
                        dashboardProductTaskStateDTO.Done = Convert.ToInt32(dr["Done"]);
                    }
                }
                con.Close();
                return dashboardProductTaskStateDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int CreateTask(SqlConnection con, ProductBacklogDataDTO Model)
        {
            int CreateTaskId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@Estimation", Model.Estimation));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogTypeId", 1));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CreateTaskId = Convert.ToInt32(dr["CreateTaskId"]);
                    }
                }
                con.Close();
                return CreateTaskId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditTask(SqlConnection con, ProductBacklogDataDTO Model, int OrgUserId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", Model.ProductBacklogDataId));
                cmd.Parameters.Add(new SqlParameter("@Estimation", Model.Estimation));
                cmd.Parameters.Add(new SqlParameter("@UsedEstimation", Model.UsedEstimation));
                cmd.Parameters.Add(new SqlParameter("@RemainingEstimation", Model.RemainingEstimation));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeleteTask(SqlConnection con, int TaskId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TaskId", TaskId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void TaskToEnhancement(SqlConnection con, int TaskId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_TaskToEnhancement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TaskId", TaskId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDataDTO> SearchTask(SqlConnection con, ProductBacklogDataDTO Model)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchTask", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
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
    }
}
