using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ASPNETFundamentals.Data;
using ASPNETFundamentals.Models;
using Microsoft.AspNetCore.Identity;
using ASPNETFundamentals.Models.ViewModels;

namespace ASPNETFundamentals.Controllers
{
    [Produces("application/json")]
    [Route("api/TicketsAPI")]
    public class TicketsAPIController : Controller
    {
        private readonly ApplicationDbContext _context;
        private UserManager<ApplicationUser> userManager;

        public TicketsAPIController(ApplicationDbContext context, UserManager<ApplicationUser> userMgr)
        {
            _context = context;
            userManager = userMgr;
        }

        // GET: api/TicketsAPI
        [HttpGet]
        public IEnumerable<Ticket> GetTickets()
        {
            return _context.Tickets;
        }

        // GET: api/TicketsAPI/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Tickets.SingleOrDefaultAsync(m => m.Id == id);

            if (ticket == null)
            {
                return NotFound();
            }

            return Ok(ticket);
        }

        // PUT: api/TicketsAPI/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTicket([FromRoute] int id, [FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != ticket.Id)
            {
                return BadRequest();
            }

            _context.Entry(ticket).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TicketExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/TicketsAPI
        [HttpPost]
        public async Task<IActionResult> PostTicket([FromBody] Ticket ticket)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Tickets.Add(ticket);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTicket", new { id = ticket.Id }, ticket);
        }

        // DELETE: api/TicketsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTicket([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ticket = await _context.Tickets.SingleOrDefaultAsync(m => m.Id == id);
            if (ticket == null)
            {
                return NotFound();
            }

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return Ok(ticket);
        }

        [HttpGet("Users")]
        public IEnumerable<TicketUsersViewModel> Users(string username)
        {
            List<TicketUsersViewModel> ticketUsers = new List<TicketUsersViewModel>();

            foreach(var user in userManager.Users.Where(u => u.UserName.Contains(username)).ToList())
            {
                var ticketUser = new TicketUsersViewModel
                {
                    Id = user.Id,
                    firstName = user.FirstName,
                    lastName = user.LastName,
                    userName = user.UserName,
                    email = user.Email
                };

                ticketUsers.Add(ticketUser);
            }

            return ticketUsers;

        }

        private bool TicketExists(int id)
        {
            return _context.Tickets.Any(e => e.Id == id);
        }
    }
}