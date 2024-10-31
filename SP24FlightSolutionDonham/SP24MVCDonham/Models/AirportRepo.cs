using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;

namespace SP24MVCDonham.Models
{
    public class AirportRepo : IAirportRepo
    {
        private ApplicationDbContext database;
        public AirportRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public Airport FindAirport(int airportID)
        {
            return this.database.Airports.Include(a => a.DepartingFlights).Include(a => a.ArrivingFlights).Where(a => a.AirportID == airportID).FirstOrDefault();
            //return this.database.Airports.Include(a => a.Flights).Where(a => a.AirportID == airportID).FirstOrDefault();
        }

        public List<Airport> ListAllAirports()
        {
            return this.database.Airports.ToList();
        }
    }
}
