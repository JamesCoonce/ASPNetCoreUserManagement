using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNETFundamentals.Models.AccountViewModels
{
    public class EditUserViewModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "User Claims")]
        public List<SelectListItem> UserClaims { get; set; }
    }
}
