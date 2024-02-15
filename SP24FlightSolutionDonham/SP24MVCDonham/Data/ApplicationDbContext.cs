using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
    }
}
