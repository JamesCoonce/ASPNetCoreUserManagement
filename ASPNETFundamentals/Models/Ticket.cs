using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models
{
    public enum Priority
    {
        HighImpact, MediumImpact, LowImpact
    }

    public enum Resolution
    {
        UnAssigned, Assigned, Testing, Completed, Rejected
    }
    public class Ticket
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Priority PriorityLevel { get; set; }
        public Resolution Resolution { get; set; }
        public ApplicationUser Resource { get; set; }
    }
}
