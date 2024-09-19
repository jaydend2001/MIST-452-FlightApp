using Microsoft.EntityFrameworkCore;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;

namespace SP24MVCDonham.Models
{
    public class PlaneRepo : IPlaneRepo
    {
        //Dependency Injection

        private ApplicationDbContext database;

        public PlaneRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }
        public List<Plane> ListAllPlanes()
        {
            return this.database.Planes.Include(p => p.Airline).ToList();
        }
    }
}
