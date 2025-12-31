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
    public class BacklogRepository
    {
        //Backlog
        public List<ProductBacklogDTO> GetAllProductBacklog(SqlConnection con, int OrgId, int PhaseId)
        {
            List<ProductBacklogDTO> productBacklogList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
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
                        productBacklogDTO.AssignedDeveloper = Convert.ToInt32(dr["AssignedDeveloper"]);
                        productBacklogDTO.AssignedDesigner = Convert.ToInt32(dr["AssignedDesigner"]);
                        productBacklogDTO.AssignedTester = Convert.ToInt32(dr["AssignedTester"]);
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
                con.Close();
                return productBacklogList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public ProductBacklogDTO GetProductBacklog(SqlConnection con, int OrgId, int ProductBacklogId)
        {
            ProductBacklogDTO productBacklogDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        productBacklogDTO = new ProductBacklogDTO();
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
                        productBacklogDTO.UsedEstimation = Convert.ToInt32(dr["UsedEstimation"]);
                        productBacklogDTO.RemainingEstimation = Convert.ToInt32(dr["RemainingEstimation"]);
                        productBacklogDTO.AssignedDeveloper = Convert.ToInt32(dr["AssignedDeveloper"]);
                        productBacklogDTO.AssignedDesigner = Convert.ToInt32(dr["AssignedDesigner"]);
                        productBacklogDTO.AssignedTester = Convert.ToInt32(dr["AssignedTester"]);

                        productBacklogDTO.AssignedDeveloperName = dr["AssignedDeveloperName"].ToString();
                        productBacklogDTO.AssignedDesignerName = dr["AssignedDesignerName"].ToString();
                        productBacklogDTO.AssignedTesterName = dr["AssignedTesterName"].ToString();
                    }
                }
                con.Close();
                return productBacklogDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DashboardProductBacklogStateDTO GetAllProductBacklogState(SqlConnection con, int OrgId, int ProjectId, int PhaseId, int OrgUserId)
        {
            DashboardProductBacklogStateDTO dashboardProductBacklogStateDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklogState", con);
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
                        dashboardProductBacklogStateDTO = new DashboardProductBacklogStateDTO();
                        dashboardProductBacklogStateDTO.TotalCount = Convert.ToInt32(dr["TotalCount"]);
                        dashboardProductBacklogStateDTO.New = Convert.ToInt32(dr["New"]);
                        dashboardProductBacklogStateDTO.Active = Convert.ToInt32(dr["Active"]);
                        dashboardProductBacklogStateDTO.Paused = Convert.ToInt32(dr["Paused"]);
                        dashboardProductBacklogStateDTO.Testing = Convert.ToInt32(dr["Testing"]);
                        dashboardProductBacklogStateDTO.Done = Convert.ToInt32(dr["Done"]);
                    }
                }
                con.Close();
                return dashboardProductBacklogStateDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int CreateProductBacklog(SqlConnection con, ProductBacklogDTO Model)
        {
            int ProductBackLogId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@AssignedDeveloper", Model.AssignedDeveloper));
                cmd.Parameters.Add(new SqlParameter("@AssignedDesigner", Model.AssignedDesigner));
                cmd.Parameters.Add(new SqlParameter("@AssignedTester", Model.AssignedTester));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", Model.CreatedBy));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@Estimation", Model.Estimation));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        ProductBackLogId = Convert.ToInt32(dr["ProductBackLogId"]);
                    }
                }
                con.Close();
                return ProductBackLogId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditProductBacklog(SqlConnection con, ProductBacklogDTO Model, int OrgUserId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@AssignedTo", Model.AssignedTo));
                cmd.Parameters.Add(new SqlParameter("@AssignedDeveloper", Model.AssignedDeveloper));
                cmd.Parameters.Add(new SqlParameter("@AssignedDesigner", Model.AssignedDesigner));
                cmd.Parameters.Add(new SqlParameter("@AssignedTester", Model.AssignedTester));
                cmd.Parameters.Add(new SqlParameter("@State", Model.State));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Estimation", Model.Estimation));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
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
        public void DeleteProductBacklog(SqlConnection con, int ProductBacklogId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDTO> SearchProductBacklog(SqlConnection con, ProductBacklogDTO Model)
        {
            List<ProductBacklogDTO> productBacklogList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchProductBacklog", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
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

        //Backlog File
        public List<ProductBacklogFileDTO> GetAllProductBacklogFile(SqlConnection con)
        {
            List<ProductBacklogFileDTO> productBacklogFileDTOs = new List<ProductBacklogFileDTO>();
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklogFile", con);
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
        public void DeleteProductBacklogFile(SqlConnection con, int ProductBacklogFileId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteProductBacklogFile", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogFileId", ProductBacklogFileId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        //Backlog User
        public List<OrgUserDTO> GetAllProductBacklogAssignedUser(SqlConnection con,int OrgId)
        {
            List<OrgUserDTO> orguserList = new List<OrgUserDTO>();
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklogAssignedUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        OrgUserDTO orgUserDTO = new OrgUserDTO();
                        orgUserDTO.OrgUserId = Convert.ToInt32(dr["OrgUserId"]);
                        orgUserDTO.UserName = dr["UserName"].ToString();
                        orguserList.Add(orgUserDTO);
                    }
                }
                con.Close();
                return orguserList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<ProductBacklogDTO> GetAllProductBacklogUserWise(SqlConnection con, int OrgId, int PhaseId, int OrgUserId)
        {
            List<ProductBacklogDTO> productBacklogList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductBacklogUserWise", con);
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
        
        //Backlog MoveTo
        public void PhaseMoveTo(SqlConnection con, int ProductBacklogId, int PhaseId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_PhaseMoveTo", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
