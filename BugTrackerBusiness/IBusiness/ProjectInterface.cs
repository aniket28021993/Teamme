using BugTrackerModels.DataModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerBusiness.IBusiness
{
    public interface ProjectInterface
    {
        List<ProjectDTO> GetAllProject();
        void CreateProject(string Description);
        void EditProject(ProjectDTO Model);
        void DeleteProject(int ProjectId);
        List<ProjectDTO> SearchProject(ProjectDTO Model);
    }
}
