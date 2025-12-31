using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BugTrackerRepository.Repository
{
    public class TestCaseRepository
    {
        #region PRODUCT TEST CASE
        public List<TestCaseDTO> GetAllTestCase(SqlConnection con, int OrgId, int ProductBacklogId)
        {
            List<TestCaseDTO> testCaseList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllTestCase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", ProductBacklogId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    testCaseList = new List<TestCaseDTO>();
                    while (dr.Read())
                    {
                        TestCaseDTO testCaseDTO = new TestCaseDTO();
                        testCaseDTO.TestCaseId = Convert.ToInt32(dr["TestCaseId"]);
                        testCaseDTO.Result = Convert.ToInt32(dr["Result"]);
                        testCaseDTO.Title = dr["Title"].ToString();
                        testCaseDTO.Description = dr["Description"].ToString();
                        testCaseDTO.CreatedName = dr["CreatedName"].ToString();
                        testCaseDTO.PreCondition = dr["PreCondition"].ToString();
                        testCaseDTO.ExpectedResult = dr["ExpectedResult"].ToString();
                        testCaseDTO.ActualResult = dr["ActualResult"].ToString();
                        testCaseList.Add(testCaseDTO);
                    }
                }
                con.Close();
                return testCaseList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public TestCaseDTO GetTestCaseData(SqlConnection con, int OrgId, int TestCaseId)
        {
            TestCaseDTO testCaseDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetTestCaseData", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", TestCaseId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        testCaseDTO = new TestCaseDTO();
                        testCaseDTO.TestCaseId = Convert.ToInt32(dr["TestCaseId"]);
                        testCaseDTO.Title = dr["Title"].ToString();
                        testCaseDTO.Description = dr["Description"].ToString();
                        testCaseDTO.PreCondition = dr["PreCondition"].ToString();                       
                        testCaseDTO.ActualResult = dr["ActualResult"].ToString();
                        testCaseDTO.ExpectedResult = dr["ExpectedResult"].ToString();
                    }
                }
                con.Close();
                return testCaseDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<TestCaseStepDTO> GetTestCaseStep(SqlConnection con, int TestCaseId)
        {
            List<TestCaseStepDTO> testCaseStepList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetTestCaseStep", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", TestCaseId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    testCaseStepList = new List<TestCaseStepDTO>();
                    while (dr.Read())
                    {
                        TestCaseStepDTO testCaseStepDTO = new TestCaseStepDTO();
                        testCaseStepDTO.TestCaseStepId = Convert.ToInt32(dr["TestCaseStepId"]);
                        testCaseStepDTO.TestCaseId = Convert.ToInt32(dr["TestCaseId"]);
                        testCaseStepDTO.Description = dr["Description"].ToString();
                        testCaseStepDTO.IsChecked = Convert.ToBoolean(dr["IsChecked"]);
                        testCaseStepDTO.IsDelete = Convert.ToBoolean(dr["IsDelete"]);
                        testCaseStepList.Add(testCaseStepDTO);
                    }
                }
                con.Close();
                return testCaseStepList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public DashboardProductTestCaseStateDTO GetAllProductTestCaseState(SqlConnection con, int OrgId, int ProjectId, int PhaseId, int OrgUserId)
        {
            DashboardProductTestCaseStateDTO dashboardProductTestCaseStateDTO = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllProductTestCaseState", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                cmd.Parameters.Add(new SqlParameter("@OrgUserId", OrgUserId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        dashboardProductTestCaseStateDTO = new DashboardProductTestCaseStateDTO();
                        dashboardProductTestCaseStateDTO.Pass = Convert.ToInt32(dr["Pass"]);
                        dashboardProductTestCaseStateDTO.Failed = Convert.ToInt32(dr["Failed"]);
                        dashboardProductTestCaseStateDTO.NotExecuted = Convert.ToInt32(dr["NotExecuted"]);
                    }
                }
                con.Close();
                return dashboardProductTestCaseStateDTO;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public int CreateTestCase(SqlConnection con, TestCaseDTO Model)
        {
            int TestCaseId = 0;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateTestCase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));

                cmd.Parameters.Add(new SqlParameter("@ExpectedResult", Model.ExpectedResult));
                cmd.Parameters.Add(new SqlParameter("@ActualResult", Model.ActualResult));
                cmd.Parameters.Add(new SqlParameter("@PreCondition", Model.PreCondition));

                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@CreatedBy", Model.CreatedBy));

                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    while (dr.Read())
                    {
                        TestCaseId = Convert.ToInt32(dr["TestCaseId"]);
                    }
                }
                con.Close();
                return TestCaseId;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void CreateTestCaseStep(SqlConnection con, TestCaseStepDTO Model, int TestCaseId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreateTestCaseStep", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", TestCaseId));

                SqlDataReader dr = cmd.ExecuteReader();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditTestCase(SqlConnection con, TestCaseDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditTestCase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", Model.TestCaseId));
                cmd.Parameters.Add(new SqlParameter("@Title", Model.Title));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@PreCondition", Model.PreCondition));
                cmd.Parameters.Add(new SqlParameter("@ActualResult", Model.ActualResult));
                cmd.Parameters.Add(new SqlParameter("@ExpectedResult", Model.ExpectedResult));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        public void EditTestCaseStep(SqlConnection con, TestCaseStepDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditTestCaseStep", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseStepId", Model.TestCaseStepId));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@IsChecked", Model.IsChecked));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        
        public void DeleteTestCase(SqlConnection con, int TestCaseId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteTestCase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", TestCaseId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeleteTestCaseStep(SqlConnection con, int TestCaseStepId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeleteTestCaseStep", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseStepId", TestCaseStepId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<TestCaseDTO> SearchTestCase(SqlConnection con, TestCaseDTO Model)
        {
            List<TestCaseDTO> testCaseList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchTestCase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@ProductBacklogId", Model.ProductBacklogId));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    testCaseList = new List<TestCaseDTO>();
                    while (dr.Read())
                    {
                        TestCaseDTO testCaseDTO = new TestCaseDTO();
                        testCaseDTO.TestCaseId = Convert.ToInt32(dr["TestCaseId"]);
                        testCaseDTO.Result = Convert.ToInt32(dr["Result"]);
                        testCaseDTO.Title = dr["Title"].ToString();
                        testCaseDTO.Description = dr["Description"].ToString();
                        testCaseDTO.CreatedName = dr["CreatedName"].ToString();
                        testCaseList.Add(testCaseDTO);
                    }
                }
                return testCaseList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void IsTestCasePass(SqlConnection con, int TestCaseId, int IsPass)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_IsTestCasePass", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@TestCaseId", TestCaseId));
                cmd.Parameters.Add(new SqlParameter("@IsPass", IsPass));
                int i = cmd.ExecuteNonQuery();
                con.Close();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
