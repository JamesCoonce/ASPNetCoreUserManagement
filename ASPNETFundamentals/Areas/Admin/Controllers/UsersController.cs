using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ASPNETFundamentals.Models;
using Microsoft.AspNetCore.Identity;
using ASPNETFundamentals.Models.AccountViewModels;

namespace ASPNETFundamentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class UsersController : Controller
    {
        private UserManager<ApplicationUser> userManager;

        public UsersController(UserManager<ApplicationUser> userMgr)
        {
            userManager = userMgr;
        }
        public IActionResult Index()
        {
            return View(userManager.Users);
        }

        public ViewResult Create() => View();
        [HttpPost]
        public async Task<IActionResult> Create(UserViewModel model)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser
                {
                    UserName = model.Name,
                    Email = model.Email
                };
                IdentityResult result = await userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    foreach (IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }

        [HttpGet] 
        public async Task<IActionResult> Edit(string id)
        {
            var model = new EditUserViewModel();
            if (id == null)
            {
                return NotFound();
            }

            var user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                model.Name = user.UserName;
                model.Email = user.Email;
                var claims = await userManager.GetClaimsAsync(user);
                /*model.UserClaims = ClaimData.UserClaims.Select(c => new SelectListItem
                {
                    Text = c,
                    Value = c,
                    Selected = claims.Any(x => x.Value == c)
                }).ToList();*/
            }
            else
            {
                /*model.UserClaims = ClaimData.UserClaims.Select(c => new SelectListItem
                {
                    Text = c,
                    Value = c
                }).ToList();*/
            }

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByIdAsync(id);
                return View(model);
            }
            return View(model);
        }
    }
}