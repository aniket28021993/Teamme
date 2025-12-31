using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class ProductBacklogDataDTO
    {
        public int ProductBacklogDataId { get; set; }
        public int ProductBacklogTypeId { get; set; }
        public string TypeDescription { get; set; }
        public int ProductBacklogId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int State { get; set; }
        public int AssignedTo { get; set; }
        public int PhaseId { get; set; }
        public int ProjectId { get; set; }
        public int OrgId { get; set; }
        public string UserName { get; set; }
        public string StateName { get; set; }
        public string SearchTask { get; set; }
        public string FileName { get; set; }
        public string RefFileName { get; set; }
        public string VideoName { get; set; }
        public string RefVideoName { get; set; }
        public int PriorityId { get; set; }
        public int Estimation { get; set; }
        public int UsedEstimation { get; set; }
        public int RemainingEstimation { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PriorityName { get; set; }
        public List<ProductBacklogFileDTO> productBacklogFileDTO { get; set; }
    }
}
