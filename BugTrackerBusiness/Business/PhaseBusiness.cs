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
    public class PhaseBusiness: PhaseInterface
    {
        private string _conn = ConfigurationManager.ConnectionStrings["ClientApp_DevEntities"].ConnectionString;
        private LoggedInUserDTO loggedInUserDTO;

        public PhaseBusiness(LoggedInUserDTO _loggedInUserDTO)
        {
            this.loggedInUserDTO = _loggedInUserDTO;
        }   
        
        public List<PhaseDTO> GetAllPhase(int ProjectId)
        {
            PhaseRepository phaseRepository = new PhaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    return phaseRepository.GetAllPhase(con, this.loggedInUserDTO._OrgId, ProjectId);
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
        public void CreatePhase(PhaseDTO Model)
        {
            PhaseRepository phaseRepository = new PhaseRepository();


            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    phaseRepository.CreatePhase(con, this.loggedInUserDTO._OrgId, Model);
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
        public void EditPhase(PhaseDTO Model)
        {
            PhaseRepository phaseRepository = new PhaseRepository();


            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    phaseRepository.EditPhase(con, Model);
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
        public void DeletePhase(int PhaseId)
        {
            PhaseRepository phaseRepository = new PhaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    phaseRepository.DeletePhase(con, PhaseId);
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
        public List<PhaseDTO> SearchPhase(PhaseDTO Model)
        {
            PhaseRepository phaseRepository = new PhaseRepository();

            using (SqlConnection con = new SqlConnection(_conn))
            {
                con.Open();
                try
                {
                    Model.OrgId = this.loggedInUserDTO._OrgId;
                    return phaseRepository.SearchPhase(con, Model);
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
