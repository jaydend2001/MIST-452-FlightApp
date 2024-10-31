using Microsoft.AspNetCore.Mvc;
using SP24MVCDonham.Models;

namespace SP24MVCDonham.Controllers
{
    public class AirportController : Controller
    {
        private IAirportRepo iAirportRepo;

        public AirportController(IAirportRepo airportRepo)
        {
            this.iAirportRepo = airportRepo;
        }

        public IActionResult ShowAirportDetails(int airportID)
        {
            return View(this.iAirportRepo.FindAirport(airportID));
        }
    }
}
