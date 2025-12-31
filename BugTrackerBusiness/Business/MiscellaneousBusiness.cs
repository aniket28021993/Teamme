using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.Business
{
    public class MiscellaneousBusiness:MiscellaneousInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;

        #region LOGIN METHODS
        public LoggedInUserDTO UserLogin(string EmailId,string Password)
        {
            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    LoggedInUserDTO loggedInUserDTO = miscellaneousRepository.UserLogin(con, EmailId,Password);

                    if (loggedInUserDTO != null)
                    {
                        #region ORG-STATUS
                        if (loggedInUserDTO._OrgStatusId == 1)
                        {
                            loggedInUserDTO._OrgErrMsg = "Organization " + loggedInUserDTO._OrgName + " is not active.";
                        }

                        else if (loggedInUserDTO._OrgStatusId == 3)
                        {
                            loggedInUserDTO._OrgErrMsg = "Organization " + loggedInUserDTO._OrgName + " is paused.";
                        }

                        else if (loggedInUserDTO._OrgStatusId == 4)
                        {
                            loggedInUserDTO._OrgErrMsg = "Organization " + loggedInUserDTO._OrgName + " is suspended.";
                        }

                        else if (loggedInUserDTO._OrgStatusId == 5)
                        {
                            loggedInUserDTO._OrgErrMsg = "Organization " + loggedInUserDTO._OrgName + " is rejected.";
                        }
                        #endregion

                        #region ORG-USER-STATUS
                        if (loggedInUserDTO._OrgUserStatusId == 1)
                        {
                            loggedInUserDTO._OrgErrMsg = loggedInUserDTO._FirstName + " " + loggedInUserDTO._LastName + " Account is not active.";
                        }

                        else if (loggedInUserDTO._OrgUserStatusId == 3)
                        {
                            loggedInUserDTO._OrgErrMsg = loggedInUserDTO._FirstName + " " + loggedInUserDTO._LastName + " Account is paused.";
                        }

                        else if (loggedInUserDTO._OrgUserStatusId == 4)
                        {
                            loggedInUserDTO._OrgErrMsg = loggedInUserDTO._FirstName + " " + loggedInUserDTO._LastName + " Account is suspended.";
                        }
                        #endregion

                        con.Open();
                        miscellaneousRepository.UserLoggedIn(con, loggedInUserDTO._OrgId, loggedInUserDTO._OrgUserId);
                    }
                    
                    return loggedInUserDTO;

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

        #endregion

        #region ADD METHODS

        public void CreateOrganization(OrganizationDTO Model)
        {
            MailBusiness mailBusiness = new MailBusiness();
            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (miscellaneousRepository.IfOrganizationExist(con, Model._OrgEmail) != 0)
                    {
                        throw new Exception("Email Id already exists.");
                    }

                    con.Open();
                    miscellaneousRepository.CreateOrganization(con, Model);
                    mailBusiness.SuccessfullRegistration(Model);
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

        public void ContactUs(ContactUsDTO Model)
        {
            MailBusiness mailBusiness = new MailBusiness();

            try
            {
               mailBusiness.ContactUs(Model);
            }
            catch (Exception Ex)
            {
               throw Ex;
            }
        }

        public void Logout(int OrgUserId,int OrgId)
        {
            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    miscellaneousRepository.Logout(con, OrgUserId,OrgId);
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

        public void SendOTP(string Email)
        {

            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                Random random = new Random();
                con.Open();
                try
                {
                    var SendOTP = random.Next(0000, 9999);
                    miscellaneousRepository.SendOTP(con, Email, SendOTP);
                    mailBusiness.SendOTP(Email, SendOTP);
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

        public int VerifyOTP(string Email,int UserOTP)
        {

            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                   return miscellaneousRepository.VerifyOTP(con, Email, UserOTP);
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

        public void ChangePassword(string Email, string Password)
        {

            MiscellaneousRepository miscellaneousRepository = new MiscellaneousRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    miscellaneousRepository.ChangePassword(con, Email, Password);
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
        #endregion

    }
}
