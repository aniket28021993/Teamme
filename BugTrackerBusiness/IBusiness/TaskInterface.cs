using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface TaskInterface
    {
        List<ProductBacklogDataDTO> GetAllProductTask(int ProductBacklogId);
        List<ProductBacklogDataDTO> GetAllDashboardProductTask(int PhaseId, int OrgUserId);
        DashboardProductTaskStateDTO GetAllProductTaskState(int ProjectId, int PhaseId, int OrgUserId);
        int CreateTask(ProductBacklogDataDTO Model);
        void EditTask(ProductBacklogDataDTO Model);
        void DeleteTask(int TaskId);
        void TaskToEnhancement(int TaskId);
        List<ProductBacklogDataDTO> SearchTask(ProductBacklogDataDTO Model);
    }
}
