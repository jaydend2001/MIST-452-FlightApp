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

        public List<Airport> ListAllAirports()
        {
            return this.database.Airports.ToList();
        }
    }
}
