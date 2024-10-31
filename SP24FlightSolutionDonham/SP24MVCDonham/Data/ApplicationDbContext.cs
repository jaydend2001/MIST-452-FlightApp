using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Data
{
    //File that represents my database
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<Airline> Airlines { get; set; }
        public DbSet<Plane> Planes { get; set; }
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Airport> Airports { get; set; }
        public DbSet<AppUser> AppUsers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Ticket> Tickets { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Airport>().HasMany(a => a.DepartingFlights).WithOne(f => f.DepartureAirport).HasForeignKey(f => f.DepartureAirportID);

            modelBuilder.Entity<Airport>().HasMany(a => a.ArrivingFlights).WithOne(f => f.ArrivalAirport).HasForeignKey(f => f.ArrivalAirportID);
        }
    }
}
