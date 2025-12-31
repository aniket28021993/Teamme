using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerRepository.Repository
{
    public class MailRepository
    {
        public string GetEmailFromOrgUser(SqlConnection con, int OrgUserId)
        {
            string EmailId = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_Mail_GetEmailFromOrgUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        EmailId = dr["EmailId"].ToString();
                    }
                }
                return EmailId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public string GetUserNameFromOrgUser(SqlConnection con, string EmailId)
        {
            string UserName = string.Empty;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_Mail_GetUserNameFromOrgUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        UserName = dr["UserName"].ToString();
                    }
                }
                return UserName;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public List<string> GetOrgData(SqlConnection con, int OrgId)
        {
            List<string> OrgData = null;

            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_Mail_GetOrgData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    OrgData = new List<string>();
                    while (dr.Read())
                    {
                        OrgData.Add(dr["OrgEmail"].ToString());
                        OrgData.Add(dr["OrgName"].ToString());
                    }
                }
                return OrgData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<string> GetOrgUserData(SqlConnection con, int OrgUserId)
        {
            List<string> OrgData = null;

            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_Mail_GetOrgUserData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    OrgData = new List<string>();
                    while (dr.Read())
                    {
                        OrgData.Add(dr["OrgEmail"].ToString());
                        OrgData.Add(dr["OrgName"].ToString());
                    }
                }
                return OrgData;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
