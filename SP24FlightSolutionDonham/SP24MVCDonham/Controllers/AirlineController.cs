using Microsoft.AspNetCore.Mvc;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;
using SP24MVCDonham.Models;

namespace SP24MVCDonham.Controllers
{
    public class AirlineController : Controller
    {
        //Dependency Injection
        //Injecting my database through the constructor
        //Controller (class) depends on my database

        /*  private ApplicationDbContext database;
          public AirlineController(ApplicationDbContext dbContext)
          {
              this.database = dbContext;
          }*/

        //Dependency Inversion
        //Controller (class) depends on interface
        private IAirlineRepo iAirlineRepo;
        public AirlineController(IAirlineRepo airlineRepo)
        {
            this.iAirlineRepo = airlineRepo;
        }

        //List all Airlines
        //no parameters -- no inputs
        //output = List<Airline>
        public IActionResult ListAllAirlines()
        {
            //Get all airlines out of DB
            //access my database
            //List<Airline> airlines = this.database.Airlines.ToList();

            //return View(model)
            List<Airline> airlines = this.iAirlineRepo.ListAllAirlines();
            return View(airlines);
        }
    }
}
