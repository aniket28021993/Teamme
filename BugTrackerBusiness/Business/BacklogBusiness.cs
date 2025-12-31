using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BugTrackerBusiness.Business
{
    public class BacklogBusiness: BacklogInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private readonly Random _random = new Random();
        private LoggedInUserDTO loggedInUserDTO;

        public BacklogBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        //Backlog
        public List<ProductBacklogDTO> GetAllProductBacklog(int PhaseId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                try
                {
                    con.Open();
                    List<ProductBacklogDTO> productBacklogDTOs = backlogRepository.GetAllProductBacklog(con, this.loggedInUserDTO._OrgId, PhaseId);

                    if (productBacklogDTOs != null)
                    {
                        con.Open();
                        List<ProductBacklogFileDTO> BacklogFile = backlogRepository.GetAllProductBacklogFile(con);

                        con.Open();
                        var BacklogAssignedUser = backlogRepository.GetAllProductBacklogAssignedUser(con,loggedInUserDTO._OrgId);

                        foreach (var item in productBacklogDTOs)
                        {
                            item.productBacklogFileDTO = BacklogFile.FindAll(x => x.ProductBacklogId == item.ProductBacklogId);

                            if(item.AssignedDesigner != 0)
                            {
                                item.AssignedDesignerName = BacklogAssignedUser.Find(x => x.OrgUserId == item.AssignedDesigner).UserName;
                            }

                            if (item.AssignedDeveloper != 0)
                            {
                                item.AssignedDeveloperName = BacklogAssignedUser.Find(x => x.OrgUserId == item.AssignedDeveloper).UserName;
                            }

                            if (item.AssignedTester != 0)
                            {
                                item.AssignedTesterName = BacklogAssignedUser.Find(x => x.OrgUserId == item.AssignedTester).UserName;
                            }
                        }
                    }

                    return productBacklogDTOs;
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
        public ProductBacklogDTO GetProductBacklog(int ProductBacklogId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var productBacklogDTOs =  backlogRepository.GetProductBacklog(con, this.loggedInUserDTO._OrgId, ProductBacklogId);

                    if (productBacklogDTOs != null)
                    {
                        con.Open();
                        List<ProductBacklogFileDTO> BacklogFile = backlogRepository.GetAllProductBacklogFile(con);

                        productBacklogDTOs.productBacklogFileDTO = BacklogFile.FindAll(x => x.ProductBacklogId == productBacklogDTOs.ProductBacklogId);
                    }

                    return productBacklogDTOs;
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
        public DashboardProductBacklogStateDTO GetAllProductBacklogState(int ProjectId, int PhaseId, int OrgUserId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return backlogRepository.GetAllProductBacklogState(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
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
        public int CreateProductBacklog(ProductBacklogDTO Model)
        {
            BacklogRepository backlogRepository = new BacklogRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    Model.CreatedBy = this.loggedInUserDTO._OrgUserId;
                    int ProductBacklogId = backlogRepository.CreateProductBacklog(con, Model);
                    mailBusiness.ProductBacklogCreate(Model);
                    return ProductBacklogId;
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
        public void EditProductBacklog(ProductBacklogDTO Model)
        {
            BacklogRepository backlogRepository = new BacklogRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    backlogRepository.EditProductBacklog(con, Model, this.loggedInUserDTO._OrgUserId, this.loggedInUserDTO._OrgId);
                    mailBusiness.ProductBacklogEdit(Model);
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
        public void DeleteProductBacklog(int ProductBacklogId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    backlogRepository.DeleteProductBacklog(con, ProductBacklogId);
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
        public List<ProductBacklogDTO> SearchProductBacklog(ProductBacklogDTO Model)
        {

            BacklogRepository backlogRepository = new BacklogRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return backlogRepository.SearchProductBacklog(con, Model);
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

        //Backlog File
        public void DeleteProductBacklogFile(int ProductBacklogFileId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    backlogRepository.DeleteProductBacklogFile(con, ProductBacklogFileId);
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

        //Backlog User
        public List<ProductBacklogDTO> GetAllProductBacklogUserWise(int PhaseId, int OrgUserId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return backlogRepository.GetAllProductBacklogUserWise(con, this.loggedInUserDTO._OrgId, PhaseId, OrgUserId);
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
 
        //Backlog MoveTo
        public void PhaseMoveTo(int ProductBacklogId, int PhaseId)
        {
            BacklogRepository backlogRepository = new BacklogRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    backlogRepository.PhaseMoveTo(con, ProductBacklogId, PhaseId);
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
