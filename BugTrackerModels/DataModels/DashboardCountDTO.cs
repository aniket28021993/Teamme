using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class DashboardCountDTO
    {
        public int PhaseCount { get; set; }
        public int ProductBacklogCount { get; set; }
        public int TaskCount { get; set; }
        public int EnhancementCount { get; set; }
        public int BugCount { get; set; }
        public int TestCaseCount { get; set; }
    }

    public class DashboardProductBacklogStateDTO
    {
        public int TotalCount { get; set; }
        public int New { get; set; }
        public int Active { get; set; }
        public int Paused { get; set; }
        public int Testing { get; set; }
        public int Done { get; set; }
    }

    public class DashboardProductTaskStateDTO
    {
        public int New { get; set; }
        public int Active { get; set; }
        public int Paused { get; set; }
        public int Done { get; set; }
    }

    public class DashboardProductEnhancementStateDTO
    {
        public int New { get; set; }
        public int Active { get; set; }
        public int Paused { get; set; }
        public int Testing { get; set; }
        public int Done { get; set; }
    }

    public class DashboardProductBugStateDTO
    {
        public int New { get; set; }
        public int Active { get; set; }
        public int Paused { get; set; }
        public int ReadyForQa { get; set; }
        public int Done { get; set; }
        public int Duplicate { get; set; }
        public int Reopen { get; set; }
    }

    public class DashboardProductTestCaseStateDTO
    {
        public int Pass { get; set; }
        public int Failed { get; set; }
        public int NotExecuted { get; set; }
    }

    public class KeyValuePair
    {
        public string Name { get; set; }
        public int Value { get; set; }

    }

    public class DashboardProductBacklogStatus
    {
        public int ProductBacklogId { get; set; }
        public string ProductBacklogName { get; set; }
        public int TaskCount { get; set; }
        public int EnhancementCount { get; set; }
        public int BugCount { get; set; }

    }

    public class DashboardPriorityBugDTO
    {
        public int TotalCount { get; set; }
        public int P1Count { get; set; }
        public int P2Count { get; set; }
        public int P3Count { get; set; }
        public int P4Count { get; set; }
    }
}
