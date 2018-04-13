using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models.ViewModels
{
    public class TicketOptionsViewModel
    {
        public List<Priority> Priorities { get; set; }
        public List<Resolution> Resolutions { get; set; }
    }
}
