using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models
{
    public class Permission
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public string Type { get; set; }
    }
}
