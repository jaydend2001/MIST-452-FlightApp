using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface IFlightRepo
    {
        public List<Flight> ListAllFlights();
        public int AddFlight(Flight flight);

        public Flight FindFlight(int flightID);

        public void EditFlight(Flight flight);
    }
}
