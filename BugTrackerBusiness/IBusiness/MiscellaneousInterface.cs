using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface MiscellaneousInterface
    {
        void CreateOrganization(OrganizationDTO Model);
        void ContactUs(ContactUsDTO model);
        LoggedInUserDTO UserLogin(string EmailId, string Password);
        void Logout(int OrgUserId, int OrgId);
        void SendOTP(string Email);
        int VerifyOTP(string Email, int UserOTP);
        void ChangePassword(string Email, string Password);
    }
}
