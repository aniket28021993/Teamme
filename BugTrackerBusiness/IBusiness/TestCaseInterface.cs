using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface TestCaseInterface
    {
        List<TestCaseDTO> GetAllTestCase(int ProductBacklogId);
        TestCaseDTO GetTestCaseData(int TestCaseId);
        DashboardProductTestCaseStateDTO GetAllProductTestCaseState(int ProjectId, int PhaseId, int OrgUserId);
        void CreateTestCase(TestCaseDTO Model);
        void CreateTestCaseStep(TestCaseStepDTO Model);
        void EditTestCase(TestCaseDTO Model);
        void DeleteTestCase(int TestCaseId);
        void DeleteTestCaseStep(int TestCaseStepId);
        List<TestCaseDTO> SearchTestCase(TestCaseDTO Model);
        void IsTestCasePass(int TestCaseId, int IsPass);
    }
}
