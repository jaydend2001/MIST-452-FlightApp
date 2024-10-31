using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using Newtonsoft.Json;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Models;
using System.Diagnostics;
using static SP24MVCDonham.Models.Chart;

namespace SP24MVCDonham.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private IAirportRepo iAirportRepo;
        private IAirlineRepo iAirlineRepo;

        public HomeController(ILogger<HomeController> logger, IAirportRepo airportRepo, IAirlineRepo airlineRepo)
        {
            _logger = logger;
            this.iAirportRepo = airportRepo;
            this.iAirlineRepo = airlineRepo;
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

            //CHART
            //Get all airlines
            List<Airline> airlines = this.iAirlineRepo.ListAllAirlines();

            //foreach airline create a data point
            List<DataPoint> dataPoints = new List<DataPoint>();
            foreach(Airline airline in airlines)
            {
                dataPoints.Add(new DataPoint(airline.AirlineName, airline.Planes.Sum(p => p.Flights.Count()), airline.AirlineID));
            }
            ViewData["airlineDataPoints"] = JsonConvert.SerializeObject(dataPoints);

            //YAML
            //YAML Aint Markup Language

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
