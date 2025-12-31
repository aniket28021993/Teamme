using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.Business
{
    public class TestCaseBusiness: TestCaseInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public TestCaseBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }

        #region PRODUCT TEST CASE
        public List<TestCaseDTO> GetAllTestCase(int ProductBacklogId)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    List<TestCaseDTO> testCaseDTOs = testcaseRepository.GetAllTestCase(con, this.loggedInUserDTO._OrgId, ProductBacklogId);

                    if (testCaseDTOs != null)
                    {
                        foreach (var item in testCaseDTOs)
                        {
                            con.Open();
                            item.TestCaseStep = testcaseRepository.GetTestCaseStep(con, item.TestCaseId);
                        }
                    }
                    return testCaseDTOs;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public TestCaseDTO GetTestCaseData(int TestCaseId)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            TestCaseDTO testCaseDTO = new TestCaseDTO();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                   testCaseDTO = testcaseRepository.GetTestCaseData(con, this.loggedInUserDTO._OrgId, TestCaseId);

                    if(testCaseDTO != null)
                    {
                        con.Open();
                        testCaseDTO.TestCaseStep = testcaseRepository.GetTestCaseStep(con, TestCaseId);
                    }
                    return testCaseDTO;
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public DashboardProductTestCaseStateDTO GetAllProductTestCaseState(int ProjectId, int PhaseId, int OrgUserId)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return testcaseRepository.GetAllProductTestCaseState(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void EditTestCase(TestCaseDTO Model)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    testcaseRepository.EditTestCase(con, Model);

                    if(Model.TestCaseStep != null)
                    {
                        foreach (var item in Model.TestCaseStep)
                        {
                            con.Open();
                            testcaseRepository.EditTestCaseStep(con,item);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void CreateTestCase(TestCaseDTO Model)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    Model.CreatedBy = this.loggedInUserDTO._OrgUserId;
                    int TestCaseId = testcaseRepository.CreateTestCase(con, Model);

                    if(Model.TestCaseStep != null)
                    {
                        foreach (var item in Model.TestCaseStep)
                        {
                            con.Open();
                            testcaseRepository.CreateTestCaseStep(con, item, TestCaseId);
                        }
                    }
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void CreateTestCaseStep(TestCaseStepDTO Model)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    testcaseRepository.CreateTestCaseStep(con, Model,Model.TestCaseId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void DeleteTestCase(int TestCaseId)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    testcaseRepository.DeleteTestCase(con, TestCaseId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void DeleteTestCaseStep(int TestCaseStepId)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    testcaseRepository.DeleteTestCaseStep(con, TestCaseStepId);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public List<TestCaseDTO> SearchTestCase(TestCaseDTO Model)
        {

            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return testcaseRepository.SearchTestCase(con, Model);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        public void IsTestCasePass(int TestCaseId, int IsPass)
        {
            TestCaseRepository testcaseRepository = new TestCaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    testcaseRepository.IsTestCasePass(con, TestCaseId, IsPass);
                }
                catch (Exception Ex)
                {
                    throw Ex;
                }
                finally
                {
                    con.Close();
                }
            }
        }
        #endregion
    }
}
