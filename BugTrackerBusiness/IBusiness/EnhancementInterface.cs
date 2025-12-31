using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface EnhancementInterface
    {
        List<ProductBacklogDataDTO> GetAllProductEnhancement(int ProductBacklogId);
        DashboardProductEnhancementStateDTO GetAllProductEnhancementState(int ProjectId, int PhaseId, int OrgUserId);
        int CreateEnhancement(ProductBacklogDataDTO Model);
        void EditEnhancement(ProductBacklogDataDTO Model);
        void DeleteEnhancement(int EnhancementId);
        void EnhancementToTaskBug(int EnhancementId, int TypeId);
        List<ProductBacklogDataDTO> SearchEnhancement(ProductBacklogDataDTO Model);
    }
}
