using Microsoft.AspNetCore.Authorization;
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
        private IPlaneRepo iPlaneRepo;
        public FlightController(IFlightRepo flightRepo, IAirportRepo airportRepo, IAirlineRepo airlineRepo, IPlaneRepo planeRepo)
        {
            this.iFlightRepo = flightRepo;
            this.iAirportRepo = airportRepo;
            this.iAirlineRepo = airlineRepo;
            this.iPlaneRepo = planeRepo;
        }

        //METHOD SIGNATURE
        //3 - Name, Return Type, Number & Order of Parameters
        //2 or more with same name = Method Overloading
        [HttpGet]
        public IActionResult SearchFlights()
        {
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.AvailableFlights = true;
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

            //if user search by flight status
            if (viewModel.FlightStatus != null)
            {
                flights = flights.Where(f => f.FlightStatus == viewModel.FlightStatus).ToList();
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

            if (viewModel.AvailableFlights)
            {
                flights = flights.Where(f => f.Plane.Capacity > 
                f.Tickets.Count).ToList();
            }

            //return result to user
            viewModel.SearchResult = flights.OrderBy(f => f.Price).ToList();
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

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddFlight()
        {
            FlightViewModel viewModel = new FlightViewModel();
            return View(viewModel);
        }
        //BUTTON
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddFlight(FlightViewModel viewModel)
        {
            if(ModelState.IsValid) 
            {
                //ADD
                //Create new object
                Flight flight = new Flight(viewModel.DepartureDateTime.Value, viewModel.ArrivalDateTime.Value, 
                    viewModel.Price.Value, viewModel.PlaneID.Value, viewModel.DepartureAirportID.Value, viewModel.ArrivalAirportID.Value);
                //Save in the database
                int flightID = this.iFlightRepo.AddFlight(flight);
                return RedirectToAction("SearchFlights");
            }
            else
            {
                //NOT ADD
                return View(viewModel);
            }
            
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditFlight(int flightID)
        {
            //Find Existing Record
            Flight flight = this.iFlightRepo.FindFlight
                (flightID);

            FlightViewModel viewModel = new FlightViewModel();
            CreateDropDownLists();

            viewModel.DepartureAirportID = flight.DepartureAirportID;
            viewModel.ArrivalAirportID = flight.ArrivalAirportID;
            viewModel.DepartureDateTime = flight.DepartureDateTime;
            viewModel.ArrivalDateTime = flight.EstimatedArrivalDateTime;
            viewModel.Price = flight.Price;
            viewModel.PlaneID = flight.PlaneID;
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditFlight(FlightViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                //Find Existing Record
                Flight flight = this.iFlightRepo.FindFlight
                    (viewModel.FlightID.Value);

                //Update Values
                flight.DepartureAirportID = viewModel.DepartureAirportID.Value;
                flight.ArrivalAirportID = viewModel.ArrivalAirportID.Value;
                flight.DepartureDateTime = viewModel.DepartureDateTime.Value;
                flight.EstimatedArrivalDateTime = viewModel.ArrivalDateTime.Value;
                flight.Price = viewModel.Price.Value;
                flight.PlaneID = viewModel.PlaneID.Value;

                //Save Changes
                this.iFlightRepo.EditFlight(flight);
                return RedirectToAction("SearchFlights");
            }
            else
            {
                CreateDropDownLists();
                return View();
            }
        }
    }
}
