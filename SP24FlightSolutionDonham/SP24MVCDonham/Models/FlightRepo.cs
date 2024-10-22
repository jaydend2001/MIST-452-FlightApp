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
            return this.database.Flights.Include(f => f.ArrivalAirport).Include(f => f.DepartureAirport).Include(f => f.Plane).ThenInclude
            (p => p.Airline).Include(f => f.Tickets).ToList();
        }

        public int AddFlight(Flight flight)
        {
            this.database.Flights.Add(flight);
            this.database.SaveChanges();

            return flight.FlightID;
        }

        public Flight FindFlight(int flightID)
        {
            return this.database.Flights.Include(f => f.Tickets).ThenInclude(t => t.AppUser).Include(f =>
            f.DepartureAirport).Include(f => f.ArrivalAirport).Include(f => f.Plane).Where(f => f.FlightID == flightID).FirstOrDefault();
        }

        public void EditFlight(Flight flight)
        {
            this.database.Flights.Update(flight);
            this.database.SaveChanges();
        }

        public void DeleteFlight(Flight flight)
        {
            this.database.Flights.Remove(flight);
            this.database.SaveChanges();
        }
    }
}
