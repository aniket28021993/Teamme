using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class MiscellaneousController : ApiController
    {
        private MiscellaneousInterface miscellaneousInterface;
        MiscellaneousController()
        {
            miscellaneousInterface = new MiscellaneousBusiness();
        }

        [HttpPost]
        public void CreateOrganization(OrganizationDTO model)
        {
            try
            {
                miscellaneousInterface.CreateOrganization(model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        public void ContactUs(ContactUsDTO model)
        {
            try
            {
                miscellaneousInterface.ContactUs(model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public void Logout(int OrgUserId,int OrgId)
        {
            try
            {
                miscellaneousInterface.Logout(OrgUserId,OrgId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public void SendOTP(string Email)
        {
            try
            {
                miscellaneousInterface.SendOTP(Email);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public int VerifyOTP(string Email,int UserOTP)
        {
            try
            {
               return miscellaneousInterface.VerifyOTP(Email,UserOTP);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        public void ChangePassword(string Email, string Password)
        {
            try
            {
                miscellaneousInterface.ChangePassword(Email, Password);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
