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

        public List<Airline> ListAllAirlines()
        {
            return this.database.Airlines.ToList();
        }
    }
}
