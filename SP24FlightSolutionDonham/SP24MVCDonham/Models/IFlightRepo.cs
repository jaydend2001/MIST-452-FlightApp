using Microsoft.AspNetCore;
using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface IFlightRepo
    {
        public List<Flight> ListAllFlights();
        public int AddFlight(Flight flight);
        public void EditFlight(Flight flight);
        public Flight FindFlight(int flightID);
        public void DeleteFlight(Flight flight);
    }
}
