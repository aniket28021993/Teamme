using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BugTrackerRepository.Repository
{
    public class GenericRepository
    {
        #region COMMENT
        public List<CommentDTO> GetAllComment(SqlConnection con, int OrgId, int CommonId, int CommentTypeId)
        {
            List<CommentDTO> commentList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllComment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@CommonId", CommonId));
                cmd.Parameters.Add(new SqlParameter("@CommentTypeId", CommentTypeId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    commentList = new List<CommentDTO>();
                    while (dr.Read())
                    {
                        CommentDTO commentDTO = new CommentDTO();
                        commentDTO.CommentId = Convert.ToInt32(dr["CommentId"]);
                        commentDTO.Description = dr["Description"].ToString();
                        commentDTO.UserId = Convert.ToInt32(dr["UserId"]);
                        commentDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        commentDTO.UserName = dr["UserName"].ToString();
                        commentList.Add(commentDTO);
                    }
                }
                return commentList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void CreateComment(SqlConnection con, CommentDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateComment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@UserId", Model.UserId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@CommentTypeId", Model.CommentTypeId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region RECENT ACTIVITY
        public List<RecentActivityDateDTO> GetAllRecentActivityDate(SqlConnection con, int OrgId, int CommonId, int RecentActivityTypeId)
        {
            List<RecentActivityDateDTO> recentActivityDateList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllRecentActivityDate", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@CommonId", CommonId));
                cmd.Parameters.Add(new SqlParameter("@RecentActivityTypeId", RecentActivityTypeId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    recentActivityDateList = new List<RecentActivityDateDTO>();
                    while (dr.Read())
                    {
                        RecentActivityDateDTO recentActivityDateDTO = new RecentActivityDateDTO();
                        recentActivityDateDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        recentActivityDateList.Add(recentActivityDateDTO);
                    }
                }
                con.Close();
                return recentActivityDateList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<RecentActivityDTO> GetAllRecentActivity(SqlConnection con, int OrgId)
        {
            List<RecentActivityDTO> recentActivityList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllRecentActivity", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    recentActivityList = new List<RecentActivityDTO>();
                    while (dr.Read())
                    {
                        RecentActivityDTO recentActivityDTO = new RecentActivityDTO();
                        recentActivityDTO.PreviousDescription = dr["PreviousDescription"].ToString();
                        recentActivityDTO.NewDescription = dr["NewDescription"].ToString();
                        recentActivityDTO.ColumnName = dr["ColumnName"].ToString();
                        recentActivityDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        recentActivityDTO.CommonId = Convert.ToInt32(dr["CommonId"]);
                        recentActivityDTO.RecentActivityTypeId = Convert.ToInt32(dr["RecentActivityTypeId"]);
                        recentActivityDTO.UserName = dr["UserName"].ToString();
                        recentActivityList.Add(recentActivityDTO);
                    }
                }
                con.Close();
                return recentActivityList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region ORG PLAN
        public void UpdateOrgPlan(SqlConnection con, int OrgId, int OrgPlanId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_UpdateOrgPlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgPlanId", OrgPlanId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region COMMON
        public List<ProductBacklogFileDTO> GetAllProductBacklogDataFile(SqlConnection con)
        {
            List<ProductBacklogFileDTO> productBacklogFileDTOs = new List<ProductBacklogFileDTO>();
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklogDataFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProductBacklogFileDTO productBacklogFileDTO = new ProductBacklogFileDTO();
                        productBacklogFileDTO.ProductBacklogFileId = Convert.ToInt16(dr["ProductBacklogFileId"]);
                        productBacklogFileDTO.ProductBacklogId = Convert.ToInt16(dr["ProductBacklogId"]);
                        productBacklogFileDTO.FileName = dr["FileName"].ToString();
                        productBacklogFileDTO.RefFileName = dr["RefFileName"].ToString();
                        productBacklogFileDTOs.Add(productBacklogFileDTO);

                    }
                }
                con.Close();
                return productBacklogFileDTOs;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public ProductBacklogDataDTO GetProductBacklogData(SqlConnection con, int OrgId, int ProductBacklogDataId)
        {
            ProductBacklogDataDTO productBacklogDataDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetProductBacklogData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataId", ProductBacklogDataId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        productBacklogDataDTO = new ProductBacklogDataDTO();
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
                        productBacklogDataDTO.PriorityId = Convert.ToInt32(dr["PriorityId"]);
                        productBacklogDataDTO.Estimation = Convert.ToInt32(dr["Estimation"]);
                        productBacklogDataDTO.UsedEstimation = Convert.ToInt32(dr["UsedEstimation"]);
                        productBacklogDataDTO.RemainingEstimation = Convert.ToInt32(dr["RemainingEstimation"]);
                    }
                }
                con.Close();
                return productBacklogDataDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeleteProductBacklogDataFile(SqlConnection con, int ProductBacklogDataFileId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteProductBacklogDataFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogDataFileId", ProductBacklogDataFileId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

    }
}
