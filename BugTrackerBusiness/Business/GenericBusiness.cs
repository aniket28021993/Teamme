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
    public class GenericBusiness: GenericInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public GenericBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }

        #region COMMENT
        public List<CommentDTO> GetAllComment(int CommonId, int CommentTypeId)
        {
            GenericRepository genericRepository = new GenericRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return genericRepository.GetAllComment(con, this.loggedInUserDTO._OrgId, CommonId, CommentTypeId);
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
        public void CreateComment(CommentDTO Model)
        {
            GenericRepository genericRepository = new GenericRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    Model.UserId = this.loggedInUserDTO._OrgUserId;
                    genericRepository.CreateComment(con, Model);
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

        #region RECENT ACTIVITY
        public List<RecentActivityDateDTO> GetAllRecentActivity(int CommonId, int RecentActivityTypeId)
        {
            GenericRepository genericRepository = new GenericRepository();

            List<RecentActivityDateDTO> recentActivityDateDTO = null;

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    recentActivityDateDTO = genericRepository.GetAllRecentActivityDate(con, this.loggedInUserDTO._OrgId, CommonId, RecentActivityTypeId);

                    if (recentActivityDateDTO != null)
                    {
                        con.Open();
                        var recentActivity = genericRepository.GetAllRecentActivity(con, this.loggedInUserDTO._OrgId);

                        if(recentActivity != null)
                        {
                            foreach (var item in recentActivityDateDTO)
                            {
                                item.recentActivityDTO = recentActivity.FindAll(x => x.CommonId == CommonId && x.RecentActivityTypeId == RecentActivityTypeId && x.CreatedOn.Date == item.CreatedOn.Date);
                            }

                        }
                    }

                    return recentActivityDateDTO;
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

        #region ORG PLAN
        public void UpdateOrgPlan(int OrgPlanId)
        {
            GenericRepository genericRepository = new GenericRepository();
            MailBusiness mailBusiness = new MailBusiness();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                try
                {
                    con.Open();
                    genericRepository.UpdateOrgPlan(con, this.loggedInUserDTO._OrgId, OrgPlanId);
                    mailBusiness.UpgradeOrgPlan(this.loggedInUserDTO, OrgPlanId);
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

        #region COMMON
        public ProductBacklogDataDTO GetProductBacklogData(int ProductBacklogDataId)
        {
            GenericRepository genericRepository = new GenericRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    var productBacklogDataDTOs = genericRepository.GetProductBacklogData(con, this.loggedInUserDTO._OrgId, ProductBacklogDataId);

                    if (productBacklogDataDTOs != null)
                    {
                        con.Open();
                        var BacklogDataFile = genericRepository.GetAllProductBacklogDataFile(con);

                        if (BacklogDataFile != null)
                        {
                            productBacklogDataDTOs.productBacklogFileDTO = BacklogDataFile.FindAll(x => x.ProductBacklogId == productBacklogDataDTOs.ProductBacklogDataId);
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
        public void DeleteProductBacklogDataFile(int ProductBacklogDataFileId)
        {
            GenericRepository genericRepository = new GenericRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    genericRepository.DeleteProductBacklogDataFile(con, ProductBacklogDataFileId);
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
