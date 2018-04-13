using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASPNETFundamentals.Data;
using ASPNETFundamentals.Models;

namespace ASPNETFundamentals.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PermissionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PermissionsController(ApplicationDbContext context)
        {
            _context = context;    
        }

        // GET: Admin/Permissions
        public async Task<IActionResult> Index()
        {
            return View(await _context.Permissions.ToListAsync());
        }

        // GET: Admin/Permissions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission = await _context.Permissions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // GET: Admin/Permissions/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Permissions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Value,Type")] Permission permission)
        {
            if (ModelState.IsValid)
            {
                _context.Add(permission);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.Id == id);
            if (permission == null)
            {
                return NotFound();
            }
            return View(permission);
        }

        // POST: Admin/Permissions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Value,Type")] Permission permission)
        {
            if (id != permission.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(permission);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PermissionExists(permission.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(permission);
        }

        // GET: Admin/Permissions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var permission = await _context.Permissions
                .SingleOrDefaultAsync(m => m.Id == id);
            if (permission == null)
            {
                return NotFound();
            }

            return View(permission);
        }

        // POST: Admin/Permissions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var permission = await _context.Permissions.SingleOrDefaultAsync(m => m.Id == id);
            _context.Permissions.Remove(permission);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool PermissionExists(int id)
        {
            return _context.Permissions.Any(e => e.Id == id);
        }
    }
}
