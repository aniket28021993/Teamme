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
    public class MiscellaneousRepository
    {
        #region LOGIN METHODS

        public LoggedInUserDTO UserLogin(SqlConnection con,string EmailId,string Password)
        {
            LoggedInUserDTO loggedInUserDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("Miscellaneous_UserLogin", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                cmd.Parameters.Add(new SqlParameter("@Password", Password));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        loggedInUserDTO = new LoggedInUserDTO();
                        loggedInUserDTO._FirstName = dr["FirstName"].ToString();
                        loggedInUserDTO._LastName = dr["LastName"].ToString();
                        loggedInUserDTO._OrgName = dr["OrgName"].ToString();
                        loggedInUserDTO._PhoneNo = dr["PhoneNo"].ToString();
                        loggedInUserDTO._ProfileImage = dr["ProfileImage"].ToString();
                        loggedInUserDTO._BioData = dr["BioData"].ToString();

                        loggedInUserDTO._OrgUserId = Convert.ToInt32(dr["OrgUserId"]);
                        loggedInUserDTO._OrgId = Convert.ToInt32(dr["OrgId"]);
                        loggedInUserDTO._OrgUserStatusId = Convert.ToInt32(dr["OrgUserStatusId"]);
                        loggedInUserDTO._OrgStatusId = Convert.ToInt32(dr["OrgStatusId"]);
                        loggedInUserDTO._OrgUserTypeId = Convert.ToInt32(dr["OrgUserTypeId"]);
                        loggedInUserDTO._OrgPlanId = Convert.ToInt32(dr["OrgPlanId"]);


                        loggedInUserDTO._EmailId = EmailId;
                    }
                }
                con.Close();
                return loggedInUserDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #endregion

        #region ADD METHODS

        public void CreateOrganization(SqlConnection con, OrganizationDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Miscellaneous_CreateOrganization", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgName", Model._OrgName));
                cmd.Parameters.Add(new SqlParameter("@OrgEmail", Model._OrgEmail));
                cmd.Parameters.Add(new SqlParameter("@OrgNumber", Model._OrgNumber));
                cmd.Parameters.Add(new SqlParameter("@OrgAddress", Model._OrgAddress));
                cmd.Parameters.Add(new SqlParameter("@OrgPlanId", Model._OrgPlanId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void Logout(SqlConnection con, int OrgUserId,int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Miscellaneous_Logout", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void UserLoggedIn(SqlConnection con, int OrgId,int OrgUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("Miscellaneous_UserLoggedIn", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void SendOTP(SqlConnection con, string Email, int OTP)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_AdminDashboard_SendOTP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@OTP", OTP));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public int VerifyOTP(SqlConnection con, string Email, int UserOTP)
        {
            int IsVerified = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_VerifyOTP", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@UserOTP", UserOTP));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        IsVerified = Convert.ToInt32(dr["IsVerified"]);
                    }
                }
                return IsVerified;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void ChangePassword(SqlConnection con, string Email, string Password)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_RestPassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Email", Email));
                cmd.Parameters.Add(new SqlParameter("@Password", Password));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public int IfOrganizationExist(SqlConnection con, string EmailId)
        {
            int UserCount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_IfOrganizationExist", con);
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
