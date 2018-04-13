using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ASPNETFundamentals.Models;
using ASPNETFundamentals.Models.AccountViewModels;
using System.ComponentModel.DataAnnotations;
using ASPNETFundamentals.Data;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace ASPNETFundamentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RolesController : Controller
    {
        private RoleManager<IdentityRole> roleManager;
        private UserManager<ApplicationUser> userManager;
        private readonly ApplicationDbContext _context;

        public RolesController(RoleManager<IdentityRole> roleMgr,
            UserManager<ApplicationUser> userMgr,
            ApplicationDbContext context)
        {
            roleManager = roleMgr;
            userManager = userMgr;
            _context = context;
        }

        public ViewResult Index() => View(roleManager.Roles);

        public IActionResult Create() => View();

        [HttpPost]
        public async Task<IActionResult> Create([Required]string name)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await roleManager.CreateAsync(new IdentityRole(name));
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            return View(name);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            IdentityRole role = await roleManager.FindByIdAsync(id);
            if (role != null)
            {
                IdentityResult result = await roleManager.DeleteAsync(role);
                if (result.Succeeded)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    AddErrorsFromResult(result);
                }
            }
            else
            {
                ModelState.AddModelError("", "No role found");
            }
            return View("Index", roleManager.Roles);
        }

        public async Task<IActionResult> Edit(string id)
        {

            IdentityRole role = await roleManager.FindByIdAsync(id);
            List<ApplicationUser> members = new List<ApplicationUser>();
            List<ApplicationUser> nonMembers = new List<ApplicationUser>();
            var permissions = role.Claims;

            foreach (ApplicationUser user in userManager.Users.ToList())
            {
                var list = await userManager.IsInRoleAsync(user, role.Name)
                    ? members : nonMembers;
                list.Add(user);
            }
            return View(new RoleEditModel
            {
                Role = role,
                Members = members,
                NonMembers = nonMembers,
                Permissions = permissions
            });
        }

        [HttpPost]
        public async Task<IActionResult> Edit(RoleModificationModel model)
        {
            IdentityResult result;
            if (ModelState.IsValid)
            {
                ApplicationUser user = new ApplicationUser();
                foreach (string userId in model.IdsToAdd ?? new string[] { })
                {
                    user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.AddToRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }
                foreach (string userId in model.IdsToDelete ?? new string[] { })
                {
                    user = await userManager.FindByIdAsync(userId);
                    if (user != null)
                    {
                        result = await userManager.RemoveFromRoleAsync(user,
                            model.RoleName);
                        if (!result.Succeeded)
                        {
                            AddErrorsFromResult(result);
                        }
                    }
                }

                IdentityRole role = await roleManager.FindByIdAsync(model.RoleId);
                if( role != null)
                {
                    foreach (int claimId in model.ClaimIdsToAdd ?? new int[] { })
                    {
                        var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.Id == claimId);
                        result = await roleManager.AddClaimAsync(role, new Claim(permission.Type, permission.Value));
                    }

                    foreach (int claimId in model.ClaimIdsToRemove ?? new int[] { })
                    {
                        
                        var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.Id == claimId);
                        var claim = role.Claims.SingleOrDefault(m => m.ClaimValue == permission.Value);
                        result = await roleManager.RemoveClaimAsync(role, claim.ToClaim());
                    }
                }

            }

            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return await Edit(model.RoleId);
            }
        }

        /*public IActionResult Permissions()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Permissions(PermissionViewModel model)
        {
            if (ModelState.IsValid)
            {
                Permission permission = new Permission
                {
                    Name = model.Name,
                    Value = model.Value
                };
            }

            return View(model);
        }*/
        private void AddErrorsFromResult(IdentityResult result)
        {
            foreach (IdentityError error in result.Errors)
            {
                ModelState.AddModelError("", error.Description);
            }
        }
    }
}