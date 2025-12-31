using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class TestCaseController : ApiController
    {
        private TestCaseInterface testcaseInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        TestCaseController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            testcaseInterface = new TestCaseBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<TestCaseDTO> GetAllTestCase(int ProductBacklogId)
        {
            try
            {
                return testcaseInterface.GetAllTestCase(ProductBacklogId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public TestCaseDTO GetTestCaseData(int TestCaseId)
        {
            try
            {
                return testcaseInterface.GetTestCaseData(TestCaseId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardProductTestCaseStateDTO GetAllProductTestCaseState(int ProjectId, int PhaseId, int OrgUserId)
        {
            try
            {
                return testcaseInterface.GetAllProductTestCaseState(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreateTestCase(TestCaseDTO Model)
        {
            try
            {
                testcaseInterface.CreateTestCase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreateTestCaseStep(TestCaseStepDTO Model)
        {
            try
            {
                testcaseInterface.CreateTestCaseStep(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        

        [HttpPost]
        [Authorize]
        public void EditTestCase(TestCaseDTO Model)
        {
            try
            {
                testcaseInterface.EditTestCase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteTestCase(int TestCaseId)
        {
            try
            {
                testcaseInterface.DeleteTestCase(TestCaseId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeleteTestCaseStep(int TestCaseStepId)
        {
            try
            {
                testcaseInterface.DeleteTestCaseStep(TestCaseStepId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<TestCaseDTO> SearchTestCase(TestCaseDTO Model)
        {
            try
            {
                return testcaseInterface.SearchTestCase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void IsTestCasePass(int TestCaseId, int IsPass)
        {
            try
            {
                testcaseInterface.IsTestCasePass(TestCaseId, IsPass);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
