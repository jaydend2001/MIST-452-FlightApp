using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Models;
using System.Diagnostics;

namespace SP24MVCDonham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAirportRepo iAirportRepo;

        public HomeController(ILogger<HomeController> logger, IAirportRepo airportRepo)
        {
            _logger = logger;
            this.iAirportRepo = airportRepo;
        }

        public IActionResult Index()
        {
            //Get airports from DB and send to the view
            List<Airport> allAirports = this.iAirportRepo.ListAllAirports();

            //For each airport, create a map marker
            List<MapMarker> mapMarkers = new List<MapMarker>();
            foreach(Airport airport in allAirports)
            {
                if (airport.Latitude != null &&  airport.Longitude != null)
                {
                    string label = "";
                    if (airport.Abbreviation == null)
                    {
                        label = airport.AirportName;
                    }
                    else
                    {
                        label = airport.Abbreviation;
                    }
                    MapMarker marker = new MapMarker(airport.AirportID, label, airport.Latitude.Value, airport.Longitude.Value);
                    mapMarkers.Add(marker);
                }
            }// end foreach
            ViewData["mapMarkers"] =  JsonConvert.SerializeObject(mapMarkers);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
