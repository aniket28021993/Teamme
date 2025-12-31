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
    public class AdminBusiness:AdminInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private readonly Random _random = new Random();

        #region GET METHODS
        public List<OrganizationDTO> GetAllOrganization()
        {
            AdminRepository adminRepository = new AdminRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    List<OrganizationDTO> organizationDTOs = adminRepository.GetAllOrganization(con);

                    return organizationDTOs;
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

        public List<OrgUserDTO> GetAllOrgUser(int OrgId)
        {
            AdminRepository adminRepository = new AdminRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return adminRepository.GetAllOrgUser(con, OrgId);
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

        public List<OrgPlanDTO> GetAllOrgPlan()
        {
            AdminRepository adminRepository = new AdminRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return adminRepository.GetAllOrgPlan(con);
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

        public List<PaymentDTO> GetAllPayment()
        {
            AdminRepository adminRepository = new AdminRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return adminRepository.GetAllPayment(con);
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
            AdminRepository adminRepository = new AdminRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return adminRepository.SearchUser(con, Model);
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

        #region UPDATE METHODS
        public void ApproveOrgPlan(int OrgId, int OrgPlanId,int UpdateOrgPlanId)
        {
            AdminRepository adminRepository = new AdminRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                   OrganizationDTO Model = adminRepository.ApproveOrgPlan(con, OrgId, OrgPlanId, UpdateOrgPlanId);
                    mailBusiness.OrgApprovePlan(Model,OrgPlanId);
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

        public void AddPayment(int OrgId, int OrgAmount)
        {
            AdminRepository adminRepository = new AdminRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    adminRepository.AddPayment(con, OrgId, OrgAmount);
                    mailBusiness.SendPayemntDetails(OrgId,OrgAmount);
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
        public void UpdateOrgStatus(int OrgId,int OrgStatusId)
        {
            AdminRepository adminRepository = new AdminRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                   adminRepository.UpdateOrgStatus(con,OrgId,OrgStatusId);
                    mailBusiness.OrgStatusChange(OrgId, OrgStatusId);
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
        public void CreateAdmin(OrgUserDTO OrgUser)
        {
            AdminRepository adminRepository = new AdminRepository();
            MailBusiness mailBusiness = new MailBusiness();

            var passwordBuilder = new StringBuilder();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    if (adminRepository.IfUserExist(con, OrgUser.EmailId) != 0)
                    {
                        throw new Exception("Email Id already exists.");
                    }

                    OrgUser.Password = passwordBuilder.Append(RandomString(4, true)).ToString() + passwordBuilder.Append(RandomNumber(0, 999)); 

                    con.Open();
                    adminRepository.CreateAdmin(con, OrgUser);
                    mailBusiness.SuccessfullAdmin(OrgUser);
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
