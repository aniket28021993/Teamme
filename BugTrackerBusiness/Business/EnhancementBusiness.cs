using BugTrackerBusiness.IBusiness;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;

namespace BugTrackerBusiness.Business
{
    public class EnhancementBusiness: EnhancementInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public EnhancementBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }
        
        public List<ProductBacklogDataDTO> GetAllProductEnhancement(int ProductBacklogId)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();
            GenericRepository genericRepository = new GenericRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    List<ProductBacklogDataDTO> productBacklogDataDTOs = enhancementRepository.GetAllProductEnhancement(con, this.loggedInUserDTO._OrgId, ProductBacklogId);

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
        public DashboardProductEnhancementStateDTO GetAllProductEnhancementState(int ProjectId, int PhaseId, int OrgUserId)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return enhancementRepository.GetAllProductEnhancementState(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
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

        public int CreateEnhancement(ProductBacklogDataDTO Model)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    int EnhancementID = enhancementRepository.CreateEnhancement(con, Model);
                    mailBusiness.ProductBacklogEnhancementCreate(Model);
                    return EnhancementID;
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
        public void EditEnhancement(ProductBacklogDataDTO Model)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();
            MailBusiness mailBusiness = new MailBusiness();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    enhancementRepository.EditEnhancement(con, Model, loggedInUserDTO._OrgUserId, loggedInUserDTO._OrgId);
                    mailBusiness.ProductBacklogEnhancementEdit(Model);
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
        public void DeleteEnhancement(int EnhancementId)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    enhancementRepository.DeleteEnhancement(con, EnhancementId);
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
        public void EnhancementToTaskBug(int EnhancementId, int TypeId)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    enhancementRepository.EnhancementToTaskBug(con, EnhancementId, TypeId);
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
        public List<ProductBacklogDataDTO> SearchEnhancement(ProductBacklogDataDTO Model)
        {
            EnhancementRepository enhancementRepository = new EnhancementRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return enhancementRepository.SearchEnhancement(con, Model);
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
