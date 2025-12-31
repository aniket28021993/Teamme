using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BugTrackerBusiness.Business
{
    public class BugBusiness: BugInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public BugBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        public List<ProductBacklogDataDTO> GetAllProductBug(int ProductBacklogId)
        {
            BugRepository bugRepository = new BugRepository();
            GenericRepository genericRepository = new GenericRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    List<ProductBacklogDataDTO> productBacklogDataDTOs = bugRepository.GetAllProductBug(con, this.loggedInUserDTO._OrgId, ProductBacklogId);
                    
                    if (productBacklogDataDTOs != null)
                    {
                        con.Open();
                        var BacklogDataFile = genericRepository.GetAllProductBacklogDataFile(con);

                        if(BacklogDataFile != null)
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
        public DashboardProductBugStateDTO GetAllProductBugState(int ProjectId, int PhaseId, int OrgUserId)
        {
            BugRepository bugRepository = new BugRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return bugRepository.GetAllProductBugState(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
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
        public int CreateBug(ProductBacklogDataDTO Model)
        {
            BugRepository bugRepository = new BugRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    int BugId = bugRepository.CreateBug(con, Model);
                    mailBusiness.ProductBacklogBugCreate(Model);
                    return BugId;
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
        public void EditBug(ProductBacklogDataDTO Model)
        {
            BugRepository bugRepository = new BugRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    bugRepository.EditBug(con, Model, this.loggedInUserDTO._OrgUserId, this.loggedInUserDTO._OrgId);
                    mailBusiness.ProductBacklogBugEdit(Model);
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
        public void DeleteBug(int BugId)
        {
            BugRepository bugRepository = new BugRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    bugRepository.DeleteBug(con, BugId);
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
        public void BugToEnhancement(int BugId)
        {
            BugRepository bugRepository = new BugRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    bugRepository.BugToEnhancement(con, BugId);
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
        public List<ProductBacklogDataDTO> SearchBug(ProductBacklogDataDTO Model)
        {

            BugRepository bugRepository = new BugRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return bugRepository.SearchBug(con, Model);
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
