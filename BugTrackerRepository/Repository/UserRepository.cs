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
    public class UserRepository
    {
        public List<OrgUserDTO> GetAllUser(SqlConnection con, int OrgId)
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
        public void CreateUser(SqlConnection con, OrgUserDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@FirstName", Model.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LastName", Model.LastName));
                cmd.Parameters.Add(new SqlParameter("@EmailId", Model.EmailId));
                cmd.Parameters.Add(new SqlParameter("@Password", Model.Password));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", Model.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@OrgUserTypeId", Model.OrgUserTypeId));

                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditUser(SqlConnection con, OrgUserDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", Model.OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@FirstName", Model.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LastName", Model.LastName));
                cmd.Parameters.Add(new SqlParameter("@EmailId", Model.EmailId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", Model.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@BioData", Model.BioData));

                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditOrgUser(SqlConnection con, OrgUserDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditOrgUser", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", Model.OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@FirstName", Model.FirstName));
                cmd.Parameters.Add(new SqlParameter("@LastName", Model.LastName));
                cmd.Parameters.Add(new SqlParameter("@EmailId", Model.EmailId));
                cmd.Parameters.Add(new SqlParameter("@PhoneNo", Model.PhoneNo));
                cmd.Parameters.Add(new SqlParameter("@OrgUserTypeId", Model.OrgUserTypeId));

                int i = cmd.ExecuteNonQuery();
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

        public void ChangeUserStatus(SqlConnection con, int StatusId, int OrgUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_ChangeUserStatus", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@StatusId", StatusId));

                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void ChangePassword(SqlConnection con, string NewPassword, int OrgUserId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_ChangePassword", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@NewPassword", NewPassword));

                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

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
        public int IfOrgUserExist(SqlConnection con, string EmailId, int OrgUserId)
        {
            int UserCount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_IfOrgUserExist", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@EmailId", EmailId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
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
        public int IfOldPasswordMatch(SqlConnection con, string OldPassword, int OrgUserId)
        {
            int UserCount = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_IfOldPasswordMatch", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                cmd.Parameters.Add(new SqlParameter("@OldPassword", OldPassword));
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

        public List<MapuserDTO> GetAllMapProjectUser(SqlConnection con, int ProjectId, int OrgId)
        {
            List<MapuserDTO> mapuserList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllMapProjectUser", con);
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
                        mapuserDTO.UserProjectMapId = Convert.ToInt32(dr["UserProjectMapId"]);
                        mapuserDTO.UserId = Convert.ToInt32(dr["UserId"]);
                        mapuserDTO.ProjectId = Convert.ToInt32(dr["ProjectId"]);
                        mapuserDTO.OrgId = Convert.ToInt32(dr["OrgId"]);
                        mapuserDTO.UserName = dr["UserName"].ToString();
                        mapuserDTO.UserTypeId = Convert.ToInt32(dr["UserTypeId"]);
                        mapuserDTO.CreatedOn = Convert.ToDateTime(dr["CreatedOn"]);
                        mapuserDTO.OrgUserStatusId = Convert.ToInt32(dr["OrgUserStatusId"]);
                        mapuserList.Add(mapuserDTO);
                    }
                }
                return mapuserList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void MapuserToProject(SqlConnection con, int UserId, int ProjectId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_MapuserToProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void RemoveuserFromProject(SqlConnection con, int UserId, int ProjectId, int OrgId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_RemoveuserFromProject", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@UserId", UserId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
