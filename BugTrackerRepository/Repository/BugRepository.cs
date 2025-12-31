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
    public class BugRepository
    {
        public List<ProductBacklogDataDTO> GetAllProductBug(SqlConnection con, int OrgId, int ProductBacklogId)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBug", con);
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
                        productBacklogDataDTO.PriorityName = dr["PriorityName"].ToString();
                        productBacklogDataDTO.PriorityId = Convert.ToInt32(dr["PriorityId"]);
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
        public DashboardProductBugStateDTO GetAllProductBugState(SqlConnection con, int OrgId, int ProjectId, int PhaseId, int OrgUserId)
        {
            DashboardProductBugStateDTO dashboardProductBugStateDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBugState", con);
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
                        dashboardProductBugStateDTO = new DashboardProductBugStateDTO();
                        dashboardProductBugStateDTO.New = Convert.ToInt32(dr["New"]);
                        dashboardProductBugStateDTO.Active = Convert.ToInt32(dr["Active"]);
                        dashboardProductBugStateDTO.Paused = Convert.ToInt32(dr["Paused"]);
                        dashboardProductBugStateDTO.ReadyForQa = Convert.ToInt32(dr["ReadyForQa"]);
                        dashboardProductBugStateDTO.Done = Convert.ToInt32(dr["Done"]);
                        dashboardProductBugStateDTO.Duplicate = Convert.ToInt32(dr["Duplicate"]);
                        dashboardProductBugStateDTO.Reopen = Convert.ToInt32(dr["Reopen"]);
                    }
                }
                con.Close();
                return dashboardProductBugStateDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int CreateBug(SqlConnection con, ProductBacklogDataDTO Model)
        {
            int CreateBugId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateBug", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogTypeId", 3));
                cmd.Parameters.Add(new SqlParameter("@PriorityId", Model.PriorityId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        CreateBugId = Convert.ToInt32(dr["CreateBugId"]);
                    }
                }
                con.Close();
                return CreateBugId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditBug(SqlConnection con, ProductBacklogDataDTO Model, int OrgUserId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditBug", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", Model.ProductBacklogDataId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@PriorityId", Model.PriorityId));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeleteBug(SqlConnection con, int BugId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteBug", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BugId", BugId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void BugToEnhancement(SqlConnection con, int BugId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_BugToEnhancement", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@BugId", BugId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDataDTO> SearchBug(SqlConnection con, ProductBacklogDataDTO Model)
        {
            List<ProductBacklogDataDTO> productBacklogDataList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchBug", con);
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
                        productBacklogDataDTO.PriorityName = dr["PriorityName"].ToString();
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
