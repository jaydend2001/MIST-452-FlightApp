using Microsoft.AspNetCore.Mvc;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;
using SP24MVCDonham.Models;
using SP24MVCDonham.ViewModels;

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

        public IActionResult ShowFlightsForAirline(int airlineID)
        {
            return View(this.iAirlineRepo.FindAirline(airlineID));
        }

        [HttpGet]
        public IActionResult AddAirline()
        {
            AirlineViewModel viewModel = new AirlineViewModel();
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult AddAirline(AirlineViewModel viewModel)
        {
            if(ModelState.IsValid)
            {
                //ADD AIRLINE
                Airline airline = new Airline(viewModel.AirlineName, viewModel.StockTicker);
                if (viewModel.Logo != null)
                {
                    using (var memoryStream = new MemoryStream())
                    {
                        viewModel.Logo.CopyTo(memoryStream);
                        airline.LogoBytes = memoryStream.ToArray();
                    }
                }
                this.iAirlineRepo.AddAirline(airline);
                return RedirectToAction("ListAllAirlines");
            }
            else
            {
                //ERROR
                return View(viewModel);
            }
        }
    }
}
