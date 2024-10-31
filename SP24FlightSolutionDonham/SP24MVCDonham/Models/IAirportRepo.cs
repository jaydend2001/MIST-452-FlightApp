using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface IAirportRepo
    {
        public List<Airport> ListAllAirports();
        public Airport FindAirport(int airportID);
    }
}
