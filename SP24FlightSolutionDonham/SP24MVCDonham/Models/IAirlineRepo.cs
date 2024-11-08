using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface IAirlineRepo
    {
        //Declare functionality
        //CRUD - Create Read Update Delete
        public List<Airline> ListAllAirlines();
        public Airline FindAirline(int airlineID);
        public void AddAirline(Airline airline);
    }
}
