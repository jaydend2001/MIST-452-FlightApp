using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
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
        private IAppUserRepo iAppUserRepo;

        public FlightController(IFlightRepo flightRepo, IAirportRepo airportRepo, 
            IAirlineRepo airlineRepo, IPlaneRepo planeRepo, IAppUserRepo 
            appUserRepo)
        {
            this.iFlightRepo = flightRepo;
            this.iAirportRepo = airportRepo;
            this.iAirlineRepo = airlineRepo;
            this.iPlaneRepo = planeRepo;
            this.iAppUserRepo = appUserRepo;
        }

        //METHOD SIGNATURE
        //3 - Name, Return Type, Number & Order of Parameters
        //2 or more with same name = Method Overloading
        [HttpGet]
        public IActionResult SearchFlights()
        {
            SearchFlightsViewModel viewModel = new SearchFlightsViewModel();
            viewModel.AvailableFlights = true;
            viewModel.FlightStatus = FlightStatus.Planned;
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
                flights = flights.Where(f => f.Plane.Capacity > f.Tickets.Count).ToList();
            }

            //return result to user
            viewModel.SearchResult = flights.OrderBy(f => f.Price).ToList();
            return View(viewModel);
        }

        public void CreateDropDownLists()
        {
            List<Airport> airports = this.iAirportRepo.ListAllAirports();
            List<Airline> airlines = this.iAirlineRepo.ListAllAirlines();
            List<Plane> planes = this.iPlaneRepo.ListAllPlanes();

            //BAG - Data structures
            ViewData["AllAirprots"] = new SelectList(airports, "AirportID", "AirportName");
            ViewData["AllAirlines"] = new SelectList(airlines, "AirlineID", "AirlineName");
            ViewData["AllPlanes"] = new SelectList(planes, "PlaneID", "PlaneInformation");
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddFlight()
        {
            FlightViewModel viewModel = new FlightViewModel();
            CreateDropDownLists();
            viewModel.DepartureDateTime = DateTime.Today;
            viewModel.ArrivalDateTime = DateTime.Today;
            return View(viewModel);
        }
        //BUTTON
        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult AddFlight(FlightViewModel viewModel)
        {
            List<Flight> existingFlights = this.iFlightRepo.ListAllFlights();
            //Duplicate Check based on PlaneID and DepartureDateTime
            if(existingFlights.Where(f => f.PlaneID == viewModel.PlaneID.Value && f.DepartureDateTime == viewModel.DepartureDateTime.Value).Any())
            {
                ModelState.AddModelError("Duplicate", "This record appears to be a duplicate! There is already a " +
                  "flight on this plane that is leaving at this time. Flight ID: " + existingFlights.Where(f =>
                  f.PlaneID == viewModel.PlaneID.Value && f.DepartureDateTime ==
                  viewModel.DepartureDateTime.Value).FirstOrDefault().FlightID);
            }
            if (ModelState.IsValid) 
            {
                string appUserID = this.iAppUserRepo.GetAppUserID();
                //ADD
                //Create new object
                Flight flight = new Flight(viewModel.DepartureDateTime.Value, viewModel.ArrivalDateTime.Value, 
                    viewModel.Price.Value, viewModel.DepartureAirportID.Value, viewModel.ArrivalAirportID.Value, viewModel.PlaneID.Value);
                //Save in the database
                int flightID = this.iFlightRepo.AddFlight(flight);
                return RedirectToAction("SearchFlights");
            }
            else
            {
                //NOT ADD
                CreateDropDownLists();
                return View(viewModel);
            }           
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditFlight(int flightID)

        {
            //Find Existing Record
            Flight flight = this.iFlightRepo.FindFlight(flightID);

            FlightViewModel viewModel = new FlightViewModel();
            CreateDropDownLists();

            viewModel.DepartureAirportID = flight.DepartureAirportID;
            viewModel.ArrivalAirportID = flight.ArrivalAirportID;
            viewModel.DepartureDateTime = flight.DepartureDateTime;
            viewModel.ArrivalDateTime = flight.EstimatedArrivalDateTime;
            viewModel.Price = flight.Price;
            viewModel.PlaneID = flight.PlaneID;

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult EditFlight(FlightViewModel viewModel)
        {
            List<Flight> existingFlights = this.iFlightRepo.ListAllFlights();
            //Duplicate Check based on PlaneID and DepartureDateTime
            if (existingFlights.Where(f => f.PlaneID == viewModel.PlaneID.Value && f.DepartureDateTime ==
            viewModel.DepartureDateTime.Value && f.FlightID != viewModel.FlightID).Any())
            {
                ModelState.AddModelError("Duplicate", "This record appears to be a duplicate! There is already a " +
                  "flight on this plane that is leaving at this time. Flight ID: " + existingFlights.Where(f =>
                  f.PlaneID == viewModel.PlaneID.Value && f.DepartureDateTime ==
                  viewModel.DepartureDateTime.Value && f.FlightID != viewModel.FlightID).FirstOrDefault().FlightID);
            }
            if (ModelState.IsValid)
            {
                //Find Existing Record
                Flight flight = this.iFlightRepo.FindFlight(viewModel.FlightID.Value);

                //Update Values
                flight.DepartureAirportID = viewModel.DepartureAirportID.Value;
                flight.ArrivalAirportID = viewModel.ArrivalAirportID.Value;
                flight.DepartureDateTime = viewModel.DepartureDateTime.Value;
                flight.EstimatedArrivalDateTime = viewModel.ArrivalDateTime.Value;
                flight.Price = viewModel.Price.Value;
                flight.PlaneID = viewModel.PlaneID.Value;

                //Save Changes
                this.iFlightRepo.EditFlight(flight);
                // return RedirectToAction("SearchFlights");
                return RedirectToAction("ShowFlightDetails", new { FlightID = viewModel.FlightID });
            }
            else
            {
                CreateDropDownLists();
                return View(viewModel);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Administrator")]
        public IActionResult ConfirmDeleteFlight(int flightID)
        {
            Flight flight = this.iFlightRepo.FindFlight (flightID);

            FlightViewModel viewModel = new FlightViewModel();
            CreateDropDownLists();

            viewModel.DepartureAirportID = flight.DepartureAirportID;
            viewModel.ArrivalAirportID = flight.ArrivalAirportID;
            viewModel.DepartureDateTime = flight.DepartureDateTime;
            viewModel.ArrivalDateTime = flight.EstimatedArrivalDateTime;
            viewModel.Price = flight.Price;
            viewModel.PlaneID = flight.PlaneID;

            return View(viewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        public IActionResult DeleteFlight(int flightID)
        {
            //Find Existing Record
            Flight flight = this.iFlightRepo.FindFlight(flightID);

            //If Flight in progress -> throw error
            if (flight.Tickets.Any())
            {
                ModelState.AddModelError("TicketSoldError", "This flight already has tickets sold!");
            }
            if(flight.FlightStatus != FlightStatus.Planned)

            {
                ModelState.AddModelError("InProgressError", "This flight is already in progress!");
            }
            if (!ModelState.IsValid)
            {
                FlightViewModel viewModel = new FlightViewModel();
                CreateDropDownLists();

                viewModel.DepartureAirportID = flight.DepartureAirportID;
                viewModel.ArrivalAirportID = flight.ArrivalAirportID;
                viewModel.DepartureDateTime = flight.DepartureDateTime;
                viewModel.ArrivalDateTime = flight.EstimatedArrivalDateTime;
                viewModel.Price = flight.Price;
                viewModel.PlaneID = flight.PlaneID;
                return View("ConfirmDeleteFlight", viewModel);
            }
            else
            {
                this.iFlightRepo.DeleteFlight(flight);
                return RedirectToAction("SearchFLights");
            }
        }

        public IActionResult ShowFlightDetails(int flightID)
        {
            Flight flight = this.iFlightRepo.FindFlight(flightID);
            //Map Markers
            string departureLabel;
            if(flight.DepartureAirport.Abbreviation == null)
            {
                departureLabel = flight.DepartureAirport.AirportName;
            }
            else
            {
                departureLabel = flight.DepartureAirport.Abbreviation;
            }

            if(flight.DepartureAirport.Latitude != null && flight.DepartureAirport.Longitude != null){
                ViewData["departureAirportMapMarker"] = JsonConvert.SerializeObject(new MapMarker(flight.DepartureAirportID, departureLabel, 
                    flight.DepartureAirport.Latitude.Value, flight.DepartureAirport.Longitude.Value));
            }

            string arrivalLabel;
            if (flight.ArrivalAirport.Abbreviation == null)
            {
                arrivalLabel = flight.ArrivalAirport.AirportName;
            }
            else
            {
                arrivalLabel = flight.ArrivalAirport.Abbreviation;
            }

            if (flight.ArrivalAirport.Latitude != null && flight.ArrivalAirport.Longitude != null)
            {
                ViewData["arrivalAirportMapMarker"] = JsonConvert.SerializeObject(new MapMarker(flight.ArrivalAirportID, arrivalLabel, flight.ArrivalAirport.Latitude.Value,
                    flight.ArrivalAirport.Longitude.Value, "Green"));
            }
            return View(flight);
        }

        [Authorize(Roles = "Administrator, Employee")]
        public IActionResult EditFlightStatus(int flightID, FlightStatus flightStatus)
        {
            try
            {
                Flight flight = this.iFlightRepo.FindFlight(flightID);
                flight.FlightStatus = flightStatus;
                this.iFlightRepo.EditFlight(flight);
                return RedirectToAction("ShowFlightDetails", new { FlightID = flightID });
            }
            catch (Exception ex)
            {
                return RedirectToAction("SearchFlights");
            }
        }
    }
}
