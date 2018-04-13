using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASPNETFundamentals.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNETFundamentals.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static async Task CreateAdminAccount(IServiceProvider serviceProvider,
                            IConfiguration configuration)
        {
            UserManager<ApplicationUser> userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager<IdentityRole> roleManager = serviceProvider.GetService<RoleManager<IdentityRole>>();

            string username = configuration["AdminUser:UserName"];
            string firstname = configuration["AdminUser:FirstName"];
            string lastname = configuration["AdminUser:LastName"];
            string email = configuration["AdminUser:Email"];
            string password = configuration["AdminUser:Password"];
            string role = configuration["AdminUser:Role"];

            if (await userManager.FindByNameAsync(username) == null)
            {
                if (await roleManager.FindByNameAsync(role) == null)
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }

                var user = new ApplicationUser { UserName = username, FirstName = firstname, LastName = lastname, Email = email };

                var result = await userManager.CreateAsync(user, password);
                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, role);
                }
            }
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            //builder.Entity<UserTicket>()
            //    .HasKey(t => new { t.UserId, t.TicketId });

            //builder.Entity<UserTicket>()
            //    .HasOne(ut => ut.User)
            //    .WithMany(u => u.Tickets)
            //    .HasForeignKey(ut => ut.UserId);

            //builder.Entity<UserTicket>()
            //    .HasOne(ut => ut.Ticket)
            //    .WithMany(t => t.Resources)
            //    .HasForeignKey(ut => ut.TicketId);

            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
        public DbSet<ASPNETFundamentals.Models.Post> Post { get; set; }
    }
}
