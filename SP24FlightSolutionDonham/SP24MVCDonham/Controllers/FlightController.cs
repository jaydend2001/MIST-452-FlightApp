using Microsoft.AspNetCore.Mvc;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Models;
using SP24MVCDonham.ViewModels;

namespace SP24MVCDonham.Controllers
{
    public class FlightController : Controller
    {

        private IFlightRepo iFlightRepo;

        public FlightController(IFlightRepo flightRepo)
        {
            this.iFlightRepo = flightRepo;
        }

        //METHOD SIGNATURE
        //3 - Name, Return Type, Number & Order of Parameters
        //2 or more with same name = Method Overloading
        [HttpGet]
        public IActionResult SearchFlights()
        {
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            return View(viewModel);
        }
        //SEARCH BUTTON
        [HttpPost]
        public IActionResult SearchFlights(SearchFlightsViewModel viewModel)//Search Inputs
        {
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

            //return result to user
            viewModel.SearchResult = flights;
            return View(viewModel);
        }
    }
}
