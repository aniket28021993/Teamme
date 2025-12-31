using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface BacklogInterface
    {
        List<ProductBacklogDTO> GetAllProductBacklog(int PhaseId);
        List<ProductBacklogDTO> GetAllProductBacklogUserWise(int PhaseId, int OrgUserId);
        DashboardProductBacklogStateDTO GetAllProductBacklogState(int ProjectId, int PhaseId, int OrgUserId);
        ProductBacklogDTO GetProductBacklog(int ProductBacklogId);
        int CreateProductBacklog(ProductBacklogDTO Model);
        void EditProductBacklog(ProductBacklogDTO Model);
        void DeleteProductBacklog(int ProductBacklogId);
        void DeleteProductBacklogFile(int ProductBacklogFileId);
        List<ProductBacklogDTO> SearchProductBacklog(ProductBacklogDTO Model);
        void PhaseMoveTo(int ProductBacklogId, int PhaseId);
    }
}
