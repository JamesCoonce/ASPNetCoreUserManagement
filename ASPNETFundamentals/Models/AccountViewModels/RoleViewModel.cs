using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models.AccountViewModels
{
    public class RoleEditModel
    {
        public IdentityRole Role { get; set; }
        public IEnumerable<ApplicationUser> Members { get; set; }
        public IEnumerable<ApplicationUser> NonMembers { get; set; }
        public IEnumerable<IdentityRoleClaim<string>> Permissions { get; set; }
    }

    public class RoleModificationModel
    {
        [Required]
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd { get; set; }
        public string[] IdsToDelete { get; set; }
        public int[] ClaimIdsToAdd { get; set; }
        public int[] ClaimIdsToRemove { get; set; }
        public List<SelectListItem> RoleClaims { get; set; }

    }
}
