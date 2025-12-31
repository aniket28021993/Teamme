using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BugTrackerBusiness.Business
{
    public class TaskBusiness: TaskInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public TaskBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        public List<ProductBacklogDataDTO> GetAllProductTask(int ProductBacklogId)
        {
            TaskRepository taskRepository = new TaskRepository();
            GenericRepository genericRepository = new GenericRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    List<ProductBacklogDataDTO> productBacklogDataDTOs = taskRepository.GetAllProductTask(con, this.loggedInUserDTO._OrgId, ProductBacklogId);

                    if (productBacklogDataDTOs != null)
                    {
                        con.Open();
                        var BacklogDataFile = genericRepository.GetAllProductBacklogDataFile(con);

                        if (BacklogDataFile != null)
                        {
                            foreach (var item in productBacklogDataDTOs)
                            {
                                item.productBacklogFileDTO = BacklogDataFile.FindAll(x => x.ProductBacklogId == item.ProductBacklogId);
                            }
                        }
                    }
                    return productBacklogDataDTOs;
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
        public List<ProductBacklogDataDTO> GetAllDashboardProductTask(int PhaseId, int OrgUserId)
        {
            TaskRepository taskRepository = new TaskRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return taskRepository.GetAllDashboardProductTask(con, this.loggedInUserDTO._OrgId, PhaseId, OrgUserId);
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
        public DashboardProductTaskStateDTO GetAllProductTaskState(int ProjectId, int PhaseId, int OrgUserId)
        {
            TaskRepository taskRepository = new TaskRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return taskRepository.GetAllProductTaskState(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
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
        public int CreateTask(ProductBacklogDataDTO Model)
        {
            TaskRepository taskRepository = new TaskRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    int TaskId = taskRepository.CreateTask(con, Model);
                    mailBusiness.ProductBacklogTaskCreate(Model);
                    return TaskId;
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
        public void EditTask(ProductBacklogDataDTO Model)
        {
            TaskRepository taskRepository = new TaskRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    taskRepository.EditTask(con, Model, loggedInUserDTO._OrgUserId, loggedInUserDTO._OrgId);
                    mailBusiness.ProductBacklogTaskEdit(Model);
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
        public void DeleteTask(int TaskId)
        {
            TaskRepository taskRepository = new TaskRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    taskRepository.DeleteTask(con, TaskId);
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
        public void TaskToEnhancement(int TaskId)
        {
            TaskRepository taskRepository = new TaskRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    taskRepository.TaskToEnhancement(con, TaskId);
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
        public List<ProductBacklogDataDTO> SearchTask(ProductBacklogDataDTO Model)
        {

            TaskRepository taskRepository = new TaskRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return taskRepository.SearchTask(con, Model);
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
        
    }
}
