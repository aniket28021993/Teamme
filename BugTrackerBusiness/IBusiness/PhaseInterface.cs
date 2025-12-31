using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface PhaseInterface
    {
        List<PhaseDTO> GetAllPhase(int ProjectId);
        void CreatePhase(PhaseDTO Model);
        void EditPhase(PhaseDTO Model);
        void DeletePhase(int PhaseId);
        List<PhaseDTO> SearchPhase(PhaseDTO Model);
    }
}
