using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class CommentDTO
    {
        public int CommentId { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public int ProductBacklogId { get; set; }
        public int OrgId { get; set; }
        public int CommentTypeId { get; set; }
        public DateTime CreatedOn { get; set; }
        public bool IsDelete { get; set; }
        public string UserName { get; set; }
    }
}
