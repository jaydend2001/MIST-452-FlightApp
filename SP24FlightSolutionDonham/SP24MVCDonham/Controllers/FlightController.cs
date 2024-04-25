using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Models;
using SP24MVCDonham.ViewModels;

namespace SP24MVCDonham.Controllers
{
    public class FlightController : Controller
    {

        private IFlightRepo iFlightRepo;
        private IAirportRepo iAirportRepo;
        private IAirlineRepo iAirlineRepo;

        public FlightController(IFlightRepo flightRepo, IAirportRepo airportRepo, IAirlineRepo airlineRepo)
        {
            this.iFlightRepo = flightRepo;
            this.iAirportRepo = airportRepo;
            this.iAirlineRepo = airlineRepo;
        }

        //METHOD SIGNATURE
        //3 - Name, Return Type, Number & Order of Parameters
        //2 or more with same name = Method Overloading
        [HttpGet]
        public IActionResult SearchFlights()
        {
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            List<Airport> airports = this.iAirportRepo.ListAllAirports();

            //BAG - Data structures
            ViewData["AllAirports"] = new SelectList(airports, "AirportID", "AirportName");

            CreateDropDownLists();

            return View(viewModel);
        }
        //SEARCH BUTTON
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsViewModel viewModel)//Search Inputs
        {
            CreateDropDownLists();

            //Get all flights
            List<Flight> flights = this.iFlightRepo.ListAllFlights();

            //if user searches by departure airport, filter result
            if(viewModel.DepartureAirportID != null)
            {
                flights = flights.Where(f => f.DepartureAirportID == viewModel.DepartureAirportID).ToList();
            }

            //if user searches by arrival airport, filter result
            if (viewModel.ArrivalAirportID != null)
            {
                flights = flights.Where(f => f.ArrivalAirportID == viewModel.ArrivalAirportID).ToList();
            }

            //if user search by airline
            if(viewModel.AirlineID != null)
            {
                flights = flights.Where(f => f.Plane.AirlineID == viewModel.AirlineID).ToList();
            }

            //if user search by start date
            if (viewModel.StartDate != null)
            {
                flights = flights.Where(f => f.DepartureDateTime >= viewModel.StartDate).ToList();
            }

            //if user search by end date
            if (viewModel.EndDate != null)
            {
                flights = flights.Where(f => f.DepartureDateTime <= viewModel.EndDate).ToList();
            }

            //if user search by airline name
            if (!String.IsNullOrEmpty(viewModel.AirlineName))
            {
                flights = flights.Where(f => f.Plane.Airline.AirlineName.ToLower().Contains(viewModel.AirlineName.Trim()
                    .ToLower())).ToList();
            }

            //return result to user
            viewModel.SearchResult = flights;
            return View(viewModel);
        }

        public void CreateDropDownLists()
        {
            List<Airport> airports = this.iAirportRepo.ListAllAirports();

            //BAG - Data structures
            ViewData["AllAirprots"] = new SelectList(airports, "AirportID", "AirportName");

            List<Airline> airlines = this.iAirlineRepo.ListAllAirlines();

            ViewData["AllAirlines"] = new SelectList(airlines, "AirlineID", "AirlineName");
        }
    }
}
