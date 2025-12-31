using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using System.Collections.Generic;

namespace BugTrackerBusiness.IBusiness
{
    public interface DashboardInterface
    {
        List<ProductBacklogDataDTO> GetAllUserDashboardProductTask(int PhaseId, int OrgUserId);

        //FILE UPLOAD
        List<ProductBacklogFileDTO> FileUpload();
        void VideoUpload();
        string ImageUpload();
        void BulkUpload();

        //COMMON METHOD

        DashboardCountDTO GetAllDashboardCount(int ProjectId, int PhaseId,int OrgUserId);

        DashboardPriorityBugDTO GetAllProductBugPriorityWise(int ProjectId, int PhaseId,int OrgUserId);
        DashboardCountDTO GetAllUserDashboardCount(int ProjectId, int PhaseId, int UserId);
        List<MapuserDTO> GetAllDashboardUser(int ProjectId);
        List<DashboardProductBacklogStatus> GetProductBacklogGraph(ProductBacklogList Model);
        List<ProductBacklogDTO> GetAllDashboardProductBacklog(int PhaseId,int OrgUserId);

        
    }
}