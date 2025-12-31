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
    public class PhaseController : ApiController
    {
        private PhaseInterface phaseInterface;
        private readonly UserLoginController loggedInObj;
        private readonly LoggedInUserDTO loggedInUserDTO;

        PhaseController()
        {
            loggedInObj = new UserLoginController();
            loggedInUserDTO = loggedInObj.LoggedInUser();
            phaseInterface = new PhaseBusiness(loggedInUserDTO);
        }
        
        [HttpGet]
        [Authorize]
        public List<PhaseDTO> GetAllPhase(int ProjectId)
        {
            try
            {
                return phaseInterface.GetAllPhase(ProjectId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void CreatePhase(PhaseDTO Model)
        {
            try
            {
                phaseInterface.CreatePhase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public void EditPhase(PhaseDTO Model)
        {
            try
            {
                phaseInterface.EditPhase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpGet]
        [Authorize]
        public void DeletePhase(int PhaseId)
        {
            try
            {
                phaseInterface.DeletePhase(PhaseId);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        [HttpPost]
        [Authorize]
        public List<PhaseDTO> SearchPhase(PhaseDTO Model)
        {
            try
            {
                return phaseInterface.SearchPhase(Model);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
