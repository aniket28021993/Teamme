using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface AdminInterface
    {
        List<OrganizationDTO> GetAllOrganization();
        List<OrgUserDTO> GetAllOrgUser(int OrgId);
        List<OrgPlanDTO> GetAllOrgPlan();
        List<PaymentDTO> GetAllPayment();

        List<OrgUserDTO> SearchUser(OrgUserDTO Model);
        void ApproveOrgPlan(int OrgId, int OrgPlanId,int UpdateOrgPlanId);
        void AddPayment(int OrgId, int OrgAmount);
        void UpdateOrgStatus(int OrgId, int OrgStatusId);
        void CreateAdmin(OrgUserDTO OrgUser);
    }
}
