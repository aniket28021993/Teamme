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
    public class AdminRepository
    {
        #region GET METHODS

        public List<OrganizationDTO> GetAllOrganization(SqlConnection con)
        {
            List<OrganizationDTO> organizationList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_GetAllOrganizations", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    organizationList = new List<OrganizationDTO>();
                    while (dr.Read())
                    {
                        OrganizationDTO organizationDTO = new OrganizationDTO();
                        organizationDTO._OrgId = Convert.ToInt32(dr["OrgId"]);
                        organizationDTO._OrgName = dr["OrgName"].ToString();
                        organizationDTO._OrgEmail = dr["OrgEmail"].ToString();
                        organizationDTO._OrgNumber = (dr["OrgNumber"]).ToString();
                        organizationDTO._OrgStatusId = Convert.ToInt32(dr["OrgStatusId"]);
                        organizationDTO._OrgPlanId = Convert.ToInt32(dr["OrgPlanId"]);
                        organizationList.Add(organizationDTO);
                    }
                }
                return organizationList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<OrgUserDTO> GetAllOrgUser(SqlConnection con, int OrgId)
        {
            List<OrgUserDTO> orgUserList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    orgUserList = new List<OrgUserDTO>();
                    while (dr.Read())
                    {
                        OrgUserDTO orgUserDTO = new OrgUserDTO();
                        orgUserDTO.OrgUserId = Convert.ToInt32(dr["OrgUserId"]);
                        orgUserDTO.OrgUserStatusId = Convert.ToInt32(dr["OrgUserStatusId"]);
                        orgUserDTO.OrgUserTypeId = Convert.ToInt32(dr["OrgUserTypeId"]);
                        orgUserDTO.FirstName = dr["FirstName"].ToString();
                        orgUserDTO.LastName = dr["LastName"].ToString();
                        orgUserDTO.EmailId = dr["EmailId"].ToString();
                        orgUserDTO.PhoneNo = dr["PhoneNo"].ToString();
                        orgUserList.Add(orgUserDTO);
                    }
                }
                return orgUserList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<OrgPlanDTO> GetAllOrgPlan(SqlConnection con)
        {
            List<OrgPlanDTO> orgPlanList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_GetAllOrgPlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    orgPlanList = new List<OrgPlanDTO>();
                    while (dr.Read())
                    {
                        OrgPlanDTO orgPlanDTO = new OrgPlanDTO();
                        orgPlanDTO.UpdateOrgPlanId = Convert.ToInt32(dr["UpdateOrgPlanId"]);
                        orgPlanDTO.OrgId = Convert.ToInt32(dr["OrgId"]);
                        orgPlanDTO.OrgName = dr["OrgName"].ToString();
                        orgPlanDTO.OrgPlanId = Convert.ToInt32(dr["OrgPlanId"]);
                        orgPlanDTO.IsApproved = Convert.ToBoolean(dr["IsApproved"]);
                        orgPlanList.Add(orgPlanDTO);
                    }
                }
                return orgPlanList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<PaymentDTO> GetAllPayment(SqlConnection con)
        {
            List<PaymentDTO> paymentList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_GetAllPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    paymentList = new List<PaymentDTO>();
                    while (dr.Read())
                    {
                        PaymentDTO paymentDTO = new PaymentDTO();
                        paymentDTO.PaymentId = Convert.ToInt32(dr["PaymentId"]);
                        paymentDTO.OrgId = Convert.ToInt32(dr["OrgId"]);
                        paymentDTO.OrgName = dr["OrgName"].ToString();
                        paymentDTO.Amount = Convert.ToInt32(dr["Amount"]);
                        paymentDTO.MonthDate = Convert.ToDateTime(dr["MonthDate"]);
                        paymentList.Add(paymentDTO);
                    }
                }
                return paymentList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<OrgUserDTO> SearchUser(SqlConnection con, OrgUserDTO Model)
        {
            List<OrgUserDTO> orgUserList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    orgUserList = new List<OrgUserDTO>();
                    while (dr.Read())
                    {
                        OrgUserDTO orgUserDTO = new OrgUserDTO();
                        orgUserDTO.OrgUserId = Convert.ToInt32(dr["OrgUserId"]);
                        orgUserDTO.OrgUserStatusId = Convert.ToInt32(dr["OrgUserStatusId"]);
                        orgUserDTO.OrgUserTypeId = Convert.ToInt32(dr["OrgUserTypeId"]);
                        orgUserDTO.FirstName = dr["FirstName"].ToString();
                        orgUserDTO.LastName = dr["LastName"].ToString();
                        orgUserDTO.EmailId = dr["EmailId"].ToString();
                        orgUserDTO.PhoneNo = dr["PhoneNo"].ToString();
                        orgUserList.Add(orgUserDTO);
                    }
                }
                return orgUserList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region UPDATE METHODS
        public OrganizationDTO ApproveOrgPlan(SqlConnection con, int OrgId, int OrgPlanId, int UpdateOrgPlanId)
        {
            OrganizationDTO OrganizationDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_ApproveOrgPlan", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgPlanId", OrgPlanId));
                cmd.Parameters.Add(new SqlParameter("@UpdateOrgPlanId", UpdateOrgPlanId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    OrganizationDTO = new OrganizationDTO();
                    while (dr.Read())
                    {
                        OrganizationDTO._OrgName = dr["OrgName"].ToString();
                        OrganizationDTO._OrgEmail = dr["OrgEmail"].ToString();
                    }
                }
                con.Close();
                return OrganizationDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void AddPayment(SqlConnection con, int OrgId, int OrgAmount)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_AddPayment", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgAmount", OrgAmount));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UpdateOrgStatus(SqlConnection con, int OrgId, int OrgStatusId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_UpdateOrgStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgStatusId", OrgStatusId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region ADD METHODS

        public void CreateAdmin(SqlConnection con, OrgUserDTO OrgUser)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_CreateAdmin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgUser.OrgId));
                cmd.Parameters.Add(new SqlParameter("@FirstName", OrgUser.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LastName", OrgUser.LastName));
                cmd.Parameters.Add(new SqlParameter("@EmailId", OrgUser.EmailId));
                cmd.Parameters.Add(new SqlParameter("@Password", OrgUser.Password));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", OrgUser.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@OrgUserStatusId", 2));
                cmd.Parameters.Add(new SqlParameter("@OrgUserTypeId", 2));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion

        #region ISCHECK
        public int IfUserExist(SqlConnection con, string EmailId)
        {
            int UserCount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_IfUserExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserCount = Convert.ToInt32(dr["UserCount"]);
                    }
                }
                con.Close();
                return UserCount;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
