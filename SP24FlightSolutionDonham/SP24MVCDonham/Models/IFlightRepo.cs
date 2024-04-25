using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface IFlightRepo
    {
        public List<Flight> ListAllFlights();
    }
}
