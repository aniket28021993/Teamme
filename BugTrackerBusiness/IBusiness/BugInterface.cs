using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface BugInterface
    {
        List<ProductBacklogDataDTO> GetAllProductBug(int ProductBacklogId);
        DashboardProductBugStateDTO GetAllProductBugState(int ProjectId, int PhaseId, int OrgUserId);
        int CreateBug(ProductBacklogDataDTO Model);
        void EditBug(ProductBacklogDataDTO Model);
        void DeleteBug(int BugId);
        void BugToEnhancement(int BugId);
        List<ProductBacklogDataDTO> SearchBug(ProductBacklogDataDTO Model);
    }
}
