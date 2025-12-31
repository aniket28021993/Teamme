using BugTrackerBusiness.Business;
using BugTrackerBusiness.IBusiness;
using BugTrackerModels.BusinessModels;
using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Http;

namespace BugtrackerController.Controllers
{
    public class DashboardController : ApiController
    {
        private DashboardInterface dashboardInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        DashboardController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            dashboardInterface = new DashboardBusiness(loggedInUserDTO);
        }

        [HttpGet]
        [Authorize]
        public List<ProductBacklogDataDTO> GetAllUserDashboardProductTask(int PhaseId, int OrgUserId)
        {
            try
            {
                return dashboardInterface.GetAllUserDashboardProductTask(PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        #region FILE
        [HttpPost]
        [Authorize]
        public List<ProductBacklogFileDTO> FileUpload()
        {
            try
            {
               return dashboardInterface.FileUpload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void VideoUpload()
        {
            try
            {
                dashboardInterface.VideoUpload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        public string ImageUpload()
        {
            try
            {
               return dashboardInterface.ImageUpload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void BulkUpload()
        {
            try
            {
                dashboardInterface.BulkUpload();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet]
        public HttpResponseMessage DownloadFile(string RefFileName,string FileName)
        {
            var result = new HttpResponseMessage();
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Uploads/" + RefFileName);
                System.IO.FileInfo _file = new System.IO.FileInfo(path);
                FileStream fileStream = System.IO.File.OpenRead(path);
                long fileLength = new FileInfo(path).Length;

                result.Content = new StreamContent(fileStream);
                result.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment");
                result.Content.Headers.ContentDisposition.FileName = FileName;
                result.Content.Headers.ContentType = new MediaTypeHeaderValue("application/.vnd.ms-excel");
                result.Content.Headers.ContentLength = fileLength;
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region COMMON METHOD

        [HttpGet]
        [Authorize]
        public DashboardCountDTO GetAllDashboardCount(int ProjectId,int PhaseId,int OrgUserId)
        {
            try
            {
              return dashboardInterface.GetAllDashboardCount(ProjectId,PhaseId,OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }


        [HttpGet]
        [Authorize]
        public DashboardPriorityBugDTO GetAllProductBugPriorityWise(int ProjectId, int PhaseId,int OrgUserId)
        {
            try
            {
                return dashboardInterface.GetAllProductBugPriorityWise(ProjectId, PhaseId, OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public DashboardCountDTO GetAllUserDashboardCount(int ProjectId, int PhaseId,int UserId)
        {
            try
            {
                return dashboardInterface.GetAllUserDashboardCount(ProjectId, PhaseId, UserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<MapuserDTO> GetAllDashboardUser(int ProjectId)
        {
            try
            {
                return dashboardInterface.GetAllDashboardUser(ProjectId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<DashboardProductBacklogStatus> GetProductBacklogGraph(ProductBacklogList Model)
        {
            try
            {
               return dashboardInterface.GetProductBacklogGraph(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public List<ProductBacklogDTO> GetAllDashboardProductBacklog(int PhaseId,int OrgUserId)
        {
            try
            {
                return dashboardInterface.GetAllDashboardProductBacklog(PhaseId,OrgUserId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        #endregion
    }
}
