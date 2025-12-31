using BugTrackerBusiness.IBusiness;
using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using BugTrackerRepository.Repository;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Text;
using System.Web;

namespace BugTrackerBusiness.Business
{
    public class DashboardBusiness: DashboardInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private readonly Random _random = new Random();
        private LoggedInUserDTO loggedInUserDTO;

        public DashboardBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }

        public List<ProductBacklogDataDTO> GetAllUserDashboardProductTask(int PhaseId, int OrgUserId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return dashboardRepository.GetAllUserDashboardProductTask(con, this.loggedInUserDTO._OrgId, PhaseId, OrgUserId);
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



        #region FILE
        public List<ProductBacklogFileDTO> FileUpload()
        {
            string returvalue = string.Empty;
            List<ProductBacklogFileDTO> productBacklogFileDTOs = new List<ProductBacklogFileDTO>();
            try
            {
                var httpRequest = HttpContext.Current.Request;
                string CommonId = System.Web.HttpContext.Current.Request.Form["CommonId"].ToString();
                string Type = System.Web.HttpContext.Current.Request.Form["Type"].ToString();

                Random random = new Random(DateTime.Now.Millisecond);
                string FileName = "";

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    FileName = Path.GetFileName(postedFile.FileName).ToString();
                    var refname = random.Next().ToString() + Path.GetExtension(postedFile.FileName.ToString());
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + refname);
                    postedFile.SaveAs(filePath);

                    DashboardRepository dashboardRepository = new DashboardRepository();
                    BacklogRepository backlogRepository = new BacklogRepository();
                    GenericRepository genericRepository = new GenericRepository();
                    using (SqlConnection con = new SqlConnection(_conn))
                    {
                        con.Open();
                        try
                        {
                            if(Type == "1")
                            {
                                dashboardRepository.FileUpload(con, Convert.ToInt32(CommonId), FileName, refname);
                                con.Open();
                                var BacklogFile = backlogRepository.GetAllProductBacklogFile(con);
                                productBacklogFileDTOs = BacklogFile.FindAll(x => x.ProductBacklogId == Convert.ToInt32(CommonId));
                            }
                            else
                            {
                                dashboardRepository.FileUploadData(con, Convert.ToInt32(CommonId), FileName, refname);
                                con.Open();
                                var BacklogFile = genericRepository.GetAllProductBacklogDataFile(con);

                                productBacklogFileDTOs = BacklogFile.FindAll(x => x.ProductBacklogId == Convert.ToInt32(CommonId));
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

                return productBacklogFileDTOs;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void VideoUpload()
        {
            string returvalue = string.Empty;
            try
            {
                var httpRequest = HttpContext.Current.Request;

                // string sPath = ConfigurationManager.AppSettings["uploadfile"].ToString();
                string CommonId = System.Web.HttpContext.Current.Request.Form["CommonId"].ToString();
                string Type = System.Web.HttpContext.Current.Request.Form["Type"].ToString();

                Random random = new Random(DateTime.Now.Millisecond);
                string FileName = "";

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    FileName = Path.GetFileName(postedFile.FileName).ToString();
                    var refname = random.Next().ToString() + Path.GetExtension(postedFile.FileName.ToString());
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + refname);
                    postedFile.SaveAs(filePath);

                    DashboardRepository dashboardRepository = new DashboardRepository();

                    using (SqlConnection con = new SqlConnection(_conn))
                    {
                        con.Open();
                        try
                        {
                            if (Type == "1")
                            {
                                dashboardRepository.VideoUpload(con, Convert.ToInt32(CommonId), FileName, refname);

                            }
                            else
                            {
                                dashboardRepository.VideoUploadData(con, Convert.ToInt32(CommonId), FileName, refname);
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

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ImageUpload()
        {
            string refname = string.Empty;
            try
            {
                var httpRequest = HttpContext.Current.Request;
                
                Random random = new Random(DateTime.Now.Millisecond);
                string FileName = "";

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    FileName = Path.GetFileName(postedFile.FileName).ToString();
                    refname = random.Next().ToString() + Path.GetExtension(postedFile.FileName.ToString());
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + refname);
                    postedFile.SaveAs(filePath);

                    DashboardRepository dashboardRepository = new DashboardRepository();

                    using (SqlConnection con = new SqlConnection(_conn))
                    {
                        con.Open();
                        try
                        {
                          dashboardRepository.ImageUpload(con,loggedInUserDTO._OrgUserId,refname);
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

                return refname;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void BulkUpload()
        {
            string returvalue = string.Empty;
            UserBusiness userBusiness = new UserBusiness(loggedInUserDTO);

            try
            {
                var httpRequest = HttpContext.Current.Request;
                
                Random random = new Random(DateTime.Now.Millisecond);
                string FileName = "";

                foreach (string file in httpRequest.Files)
                {
                    var postedFile = httpRequest.Files[file];

                    //Save File
                    FileName = Path.GetFileName(postedFile.FileName).ToString();
                    var refname = random.Next().ToString() + Path.GetExtension(postedFile.FileName.ToString());
                    var filePath = HttpContext.Current.Server.MapPath("~/Uploads/" + refname);
                    postedFile.SaveAs(filePath);

                    //Read File
                    string path = HttpContext.Current.Server.MapPath("~/Uploads/" + refname);
                    string[] lines = System.IO.File.ReadAllLines(path);
                    foreach (string line in lines)
                    {
                        string[] columns = line.Split(',');
                        if(columns != null)
                        {
                            OrgUserDTO orgUserDTO = new OrgUserDTO();
                            orgUserDTO.FirstName = columns[0];
                            orgUserDTO.LastName = columns[1];
                            orgUserDTO.EmailId = columns[2];
                            orgUserDTO.PhoneNo = columns[3];

                            if(columns[4].ToUpper() == "MANAGER")
                            {
                                orgUserDTO.OrgUserTypeId = 3;
                            }
                            else if (columns[4].ToUpper() == "TEAM LEAD" || columns[4].ToUpper() == "TEAMLEAD")
                            {
                                orgUserDTO.OrgUserTypeId = 4;
                            }
                            else if (columns[4].ToUpper() == "TEAM MEMBER" || columns[4].ToUpper() == "TEAMMEMBER")
                            {
                                orgUserDTO.OrgUserTypeId = 5;
                            }                           
                            orgUserDTO.OrgId = this.loggedInUserDTO._OrgId;
                            orgUserDTO.Password = "Pass@123";
                            userBusiness.CreateUser(orgUserDTO);
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region COMMON METHOD

        public DashboardCountDTO GetAllDashboardCount(int ProjectId,int PhaseId,int OrgUserId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return dashboardRepository.GetAllDashboardCount(con, this.loggedInUserDTO._OrgId, ProjectId,PhaseId, OrgUserId);
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

        public DashboardPriorityBugDTO GetAllProductBugPriorityWise(int ProjectId, int PhaseId,int OrgUserId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return dashboardRepository.GetAllProductBugPriorityWise(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId, OrgUserId);
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
        public DashboardCountDTO GetAllUserDashboardCount(int ProjectId, int PhaseId,int UserId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();
            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    //return dashboardRepository.GetAllUserDashboardCount(con, this.loggedInUserDTO._OrgId, ProjectId, PhaseId,UserId);
                    return null;
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
        public List<MapuserDTO> GetAllDashboardUser(int ProjectId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                List<MapuserDTO> mapuserDTOs = null;
                con.Open();
                try
                {
                    mapuserDTOs = dashboardRepository.GetAllDashboardUser(con, ProjectId, this.loggedInUserDTO._OrgId);

                    if(mapuserDTOs != null)
                    {
                        foreach (var item in mapuserDTOs)
                        {
                            con.Open();
                            dashboardRepository.GetAllDashboardUserRemainingCount(con, item);
                        }
                    }
                    
                    return mapuserDTOs;
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
        public List<DashboardProductBacklogStatus> GetProductBacklogGraph(ProductBacklogList Model)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();

            List<DashboardProductBacklogStatus> productBacklogList = null;

            using (SqlConnection con = new SqlConnection(_conn))
            {
                try
                {
                    if (Model != null)
                    {
                        productBacklogList = new List<DashboardProductBacklogStatus>();
                        foreach (var item in Model.Backlog)
                        {
                            DashboardProductBacklogStatus dashboardProductBacklogStatus = new DashboardProductBacklogStatus();
                            dashboardProductBacklogStatus.ProductBacklogId = item.id;

                            con.Open();
                            dashboardProductBacklogStatus = dashboardRepository.GetAllDashboardProductBacklogGraph(con, dashboardProductBacklogStatus);
                            productBacklogList.Add(dashboardProductBacklogStatus);
                        }
                    }


                    return productBacklogList;
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

        public List<ProductBacklogDTO> GetAllDashboardProductBacklog(int PhaseId,int OrgUserId)
        {
            DashboardRepository dashboardRepository = new DashboardRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return dashboardRepository.GetAllDashboardProductBacklog(con, this.loggedInUserDTO._OrgId, PhaseId,OrgUserId);
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
