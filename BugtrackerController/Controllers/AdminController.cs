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
    public class AdminController : ApiController
    {
        private AdminInterface adminInterface;
        AdminController()
        {
            adminInterface = new AdminBusiness();
        }

        [HttpGet]
        [Authorize]
        public List<OrganizationDTO> GetAllOrganization()
        {
            try
            {
               return adminInterface.GetAllOrganization();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<OrgUserDTO> GetAllOrgUser(int OrgId)
        {
            try
            {
                return adminInterface.GetAllOrgUser(OrgId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<OrgPlanDTO> GetAllOrgPlan()
        {
            try
            {
                return adminInterface.GetAllOrgPlan();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<PaymentDTO> GetAllPayment()
        {
            try
            {
                return adminInterface.GetAllPayment();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<OrgUserDTO> SearchUser(OrgUserDTO Model)
        {
            try
            {
                return adminInterface.SearchUser(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void ApproveOrgPlan(int OrgId, int OrgPlanId,int UpdateOrgPlanId)
        {
            try
            {
                adminInterface.ApproveOrgPlan(OrgId, OrgPlanId, UpdateOrgPlanId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void AddPayment(int OrgId, int OrgAmount)
        {
            try
            {
                adminInterface.AddPayment(OrgId, OrgAmount);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void UpdateOrgStatus(int OrgId,int OrgStatusId)
        {
            try
            {
                adminInterface.UpdateOrgStatus(OrgId,OrgStatusId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreateAdmin(OrgUserDTO OrgUser)
        {
            try
            {
                adminInterface.CreateAdmin(OrgUser);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
