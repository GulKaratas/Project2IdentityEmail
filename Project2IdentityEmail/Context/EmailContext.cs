using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Project2IdentityEmail.Entities;

namespace Project2IdentityEmail.Context
{
    public class EmailContext : IdentityDbContext<AppUser>
    {

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Initial Catalog=Project2EmailDb;Integrated Security=True;TrustServerCertificate=True");
        }

        public DbSet<Message> Messages { get; set; }
    }
}
