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
    public class EnhancementRepository
    {
        public List<ProductBacklogDataDTO> GetAllProductEnhancement(SqlConnection con, int OrgId, int ProductBacklogId)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductEnhancement", con);
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
        public DashboardProductEnhancementStateDTO GetAllProductEnhancementState(SqlConnection con, int OrgId, int ProjectId, int PhaseId, int OrgUserId)
        {
            DashboardProductEnhancementStateDTO dashboardProductEnhancementStateDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductEnhancementState", con);
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
                        dashboardProductEnhancementStateDTO = new DashboardProductEnhancementStateDTO();
                        dashboardProductEnhancementStateDTO.New = Convert.ToInt32(dr["New"]);
                        dashboardProductEnhancementStateDTO.Active = Convert.ToInt32(dr["Active"]);
                        dashboardProductEnhancementStateDTO.Paused = Convert.ToInt32(dr["Paused"]);
                        dashboardProductEnhancementStateDTO.Testing = Convert.ToInt32(dr["Testing"]);
                        dashboardProductEnhancementStateDTO.Done = Convert.ToInt32(dr["Done"]);
                    }
                }
                con.Close();
                return dashboardProductEnhancementStateDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int CreateEnhancement(SqlConnection con, ProductBacklogDataDTO Model)
        {
            int CreateEnhancementId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateEnhancement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogTypeId", 2));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CreateEnhancementId = Convert.ToInt32(dr["CreateEnhancementId"]);
                    }
                }
                con.Close();
                return CreateEnhancementId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditEnhancement(SqlConnection con, ProductBacklogDataDTO Model, int OrgUserId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditEnhancement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", Model.ProductBacklogDataId));
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
        public void DeleteEnhancement(SqlConnection con, int EnhancementId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteEnhancement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EnhancementId", EnhancementId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EnhancementToTaskBug(SqlConnection con, int EnhancementId, int TypeId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EnhancementToTaskBug", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EnhancementId", EnhancementId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogTypeId", TypeId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDataDTO> SearchEnhancement(SqlConnection con, ProductBacklogDataDTO Model)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchEnhancement", con);
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
