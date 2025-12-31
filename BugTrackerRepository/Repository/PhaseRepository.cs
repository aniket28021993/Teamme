using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BugTrackerRepository.Repository
{
    public class PhaseRepository
    {
        public List<PhaseDTO> GetAllPhase(SqlConnection con, int OrgId, int ProjectId)
        {
            List<PhaseDTO> phaseList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_GetAllPhase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", ProjectId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    phaseList = new List<PhaseDTO>();
                    while (dr.Read())
                    {
                        PhaseDTO phaseDTO = new PhaseDTO();
                        phaseDTO.PhaseId = Convert.ToInt32(dr["PhaseId"]);
                        phaseDTO.ProjectId = Convert.ToInt32(dr["ProjectId"]);
                        phaseDTO.OrgId = Convert.ToInt32(dr["OrgId"]);
                        phaseDTO.Description = dr["Description"].ToString();
                        phaseDTO.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        phaseDTO.EndDate = Convert.ToDateTime(dr["EndDate"]);
                        phaseList.Add(phaseDTO);
                    }
                }
                return phaseList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void CreatePhase(SqlConnection con, int OrgId, PhaseDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_CreatePhase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@OrgId", OrgId));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@StartDate", Model.StartDate));
                cmd.Parameters.Add(new SqlParameter("@EndDate", Model.EndDate));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void EditPhase(SqlConnection con, PhaseDTO Model)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_EditPhase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@Description", Model.Description));
                cmd.Parameters.Add(new SqlParameter("@PhaseId", Model.PhaseId));
                cmd.Parameters.Add(new SqlParameter("@StartDate", Model.StartDate));
                cmd.Parameters.Add(new SqlParameter("@EndDate", Model.EndDate));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public void DeletePhase(SqlConnection con, int PhaseId)
        {
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_DeletePhase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@PhaseId", PhaseId));
                int i = cmd.ExecuteNonQuery();
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
        public List<PhaseDTO> SearchPhase(SqlConnection con, PhaseDTO Model)
        {
            List<PhaseDTO> phaseList = null;
            try
            {
                SqlCommand cmd = new SqlCommand("BugTracker_ClientDashboard_SearchPhase", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(new SqlParameter("@SearchTask", Model.SearchTask));
                cmd.Parameters.Add(new SqlParameter("@ProjectId", Model.ProjectId));
                cmd.Parameters.Add(new SqlParameter("@OrgId", Model.OrgId));
                SqlDataReader dr = cmd.ExecuteReader();
                if (dr.HasRows)
                {
                    phaseList = new List<PhaseDTO>();
                    while (dr.Read())
                    {
                        PhaseDTO phaseDTO = new PhaseDTO();
                        phaseDTO.PhaseId = Convert.ToInt32(dr["PhaseId"]);
                        phaseDTO.Description = dr["Description"].ToString();
                        phaseDTO.StartDate = Convert.ToDateTime(dr["StartDate"]);
                        phaseDTO.EndDate = Convert.ToDateTime(dr["EndDate"]);
                        phaseList.Add(phaseDTO);
                    }
                }
                return phaseList;
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }
    }
}
