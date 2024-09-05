using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;

namespace SP24MVCDonham.Models
{
    public class FlightRepo : IFlightRepo
    {
        private ApplicationDbContext database;

        public FlightRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public List<Flight> ListAllFlights()
        {
            return this.database.Flights.Include(f => f.ArrivalAirport).Include(f => 
            f.DepartureAirport).Include(f => f.Plane).ThenInclude(p => p.Airline).Include(f => f.Tickets).ToList();
        }
    }
}
