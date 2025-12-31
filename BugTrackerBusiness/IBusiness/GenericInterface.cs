using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface GenericInterface
    {
        //COMMENT
        List<CommentDTO> GetAllComment(int CommonId, int CommentTypeId);
        void CreateComment(CommentDTO Model);

        //RECENT ACTIVITY
        List<RecentActivityDateDTO> GetAllRecentActivity(int CommonId, int RecentActivityTypeId);

        //ORG PLAN
        void UpdateOrgPlan(int OrgPlanId);

        //COMMON
        ProductBacklogDataDTO GetProductBacklogData(int ProductBacklogDataId);
        void DeleteProductBacklogDataFile(int ProductBacklogDataFileId);
    }
}
