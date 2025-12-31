using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class TestCaseDTO
    {
        public int TestCaseId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string PreCondition { get; set; }
        public string ExpectedResult { get; set; }
        public string ActualResult { get; set; }       
        public int ProductBacklogId { get; set; }
        public int PhaseId { get; set; }
        public int ProjectId { get; set; }
        public int OrgId { get; set; }
        public string SearchTask { get; set; }
        public int Result { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedName { get; set; }
        public List<TestCaseStepDTO> TestCaseStep { get; set; }
    }

    public class TestCaseStepDTO
    {
        public int TestCaseStepId { get; set; }
        public int TestCaseId { get; set; }
        public string Description { get; set; }
        public bool IsChecked { get; set; }
        public bool IsDelete { get; set; }
    }
}
