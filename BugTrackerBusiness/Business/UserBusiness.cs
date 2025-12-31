using BugTrackerBusiness.IBusiness;
using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;

namespace BugTrackerBusiness.Business
{
    public class UserBusiness: UserInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;
        private readonly Random _random = new Random();

        public UserBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        public List<OrgUserDTO> GetAllUser()
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return userRepository.GetAllUser(con, this.loggedInUserDTO._OrgId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void CreateUser(OrgUserDTO Model)
        {
            UserRepository userRepository = new UserRepository();
            MailBusiness mailBusiness = new MailBusiness();

            var passwordBuilder = new StringBuilder();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (userRepository.IfUserExist(con, Model.EmailId) != 0)
                    {
                        throw new Exception("Email Id already exists.");
                    }

                    con.Open();
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    Model.Password = passwordBuilder.Append(RandomString(4, true)).ToString() + passwordBuilder.Append(RandomNumber(0, 999));
                    userRepository.CreateUser(con, Model);
                    mailBusiness.SuccessfullUser(Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void EditUser(OrgUserDTO Model)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (this.loggedInUserDTO._EmailId != Model.EmailId)
                    {
                        if (userRepository.IfUserExist(con, Model.EmailId) != 0)
                        {
                            throw new Exception("Email Id already exists.");
                        }
                        con.Open();
                    }

                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    Model.OrgUserId = this.loggedInUserDTO._OrgUserId;
                    userRepository.EditUser(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void EditOrgUser(OrgUserDTO Model)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (userRepository.IfOrgUserExist(con, Model.EmailId, Model.OrgUserId) != 0)
                    {
                        throw new Exception("Email Id already exists.");
                    }

                    con.Open();
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    userRepository.EditOrgUser(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public List<OrgUserDTO> SearchUser(OrgUserDTO Model)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return userRepository.SearchUser(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public void ChangeUserStatus(int StatusId, int OrgUserId)
        {
            UserRepository userRepository = new UserRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    userRepository.ChangeUserStatus(con, StatusId, OrgUserId);
                    mailBusiness.OrgUserStatusChange(OrgUserId, StatusId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void ChangePassword(string NewPassword, string OldPassword)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (userRepository.IfOldPasswordMatch(con, OldPassword, this.loggedInUserDTO._OrgUserId) == 0)
                    {
                        throw new Exception("Old Password does not match.");
                    }

                    con.Open();
                    userRepository.ChangePassword(con, NewPassword, this.loggedInUserDTO._OrgUserId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        
        public List<MapuserDTO> GetAllMapUser(int ProjectId)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {

                    return userRepository.GetAllMapProjectUser(con, ProjectId, this.loggedInUserDTO._OrgId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void MapuserToProject(MapUserBM Model)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    foreach (var item in Model.Users)
                    {
                        if (item.text == true)
                        {
                            userRepository.MapuserToProject(con, item.id, Model.ProjectId, this.loggedInUserDTO._OrgId);
                        }
                        else
                        {
                            userRepository.RemoveuserFromProject(con, item.id, Model.ProjectId, this.loggedInUserDTO._OrgId);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void RemoveuserFromProject(MapUserBM Model)
        {
            UserRepository userRepository = new UserRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    foreach (var item in Model.Users)
                    {
                        userRepository.RemoveuserFromProject(con, item.id, Model.ProjectId, this.loggedInUserDTO._OrgId);
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public string RandomString(int size, bool lowerCase = false)
        {
            var builder = new StringBuilder(size);

            char offset = lowerCase ? 'a' : 'A';
            const int lettersOffset = 26;

            for (var i = 0; i < size; i++)
            {
                var @char = (char)_random.Next(offset, offset + lettersOffset);
                builder.Append(@char);
            }

            return lowerCase ? builder.ToString().ToLower() : builder.ToString();
        }
        public int RandomNumber(int min, int max)
        {
            return _random.Next(min, max);
        }
    }
}
