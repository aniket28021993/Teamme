using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BugTrackerModels.DataModels
{
    public class PaymentDTO
    {
        public int PaymentId { get; set; }
        public int OrgId { get; set; }
        public string OrgName { get; set; }
        public int Amount { get; set; }
        public DateTime MonthDate { get; set; }
    }
}
