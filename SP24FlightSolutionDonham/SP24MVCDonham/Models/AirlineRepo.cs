using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;

namespace SP24MVCDonham.Models
{
    public class AirlineRepo : IAirlineRepo
    {
        //Dependency Injection - Database
        private ApplicationDbContext database;

        public AirlineRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public void AddAirline(Airline airline)
        {
            this.database.Airlines.Add(airline);
            this.database.SaveChanges();
        }

        public Airline FindAirline(int airlineID)
        {
            return this.database.Airlines.Include(airlineID => airlineID.Planes).ThenInclude(p => p.Flights).Where(a => a.AirlineID == airlineID).FirstOrDefault();
        }

        public List<Airline> ListAllAirlines()
        {
            return this.database.Airlines.Include(a => a.Planes).ThenInclude(p => p.Flights).ToList();
        }
    }
}
