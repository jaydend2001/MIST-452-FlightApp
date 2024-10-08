using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using SP24ClassLibraryDonham;
using SP24MVCDonham.Models;

namespace SP24MVCDonham.Controllers
{
    public class TicketController : Controller
    {
        private IAppUserRepo iAppUserRepo;
        private IFlightRepo iFLightRepo;
        private ITicketRepo iTicketRepo;

        public TicketController(IAppUserRepo appUserRepo, IFlightRepo flightRepo, ITicketRepo ticketRepo)
        {
            this.iAppUserRepo = appUserRepo;
            this.iFLightRepo = flightRepo;
            this.iTicketRepo = ticketRepo;
        }

        public IActionResult PurchaseTicket(int flightID)
        {
            try
            {
                //PRECHECKS
                Flight flight = this.iFLightRepo.FindFlight(flightID);
                if (flight.FlightStatus != FlightStatus.Planned && flight.FlightStatus != FlightStatus.Delayed)
                {
                    //SHOULD NOT PURCHASE
                    return RedirectToAction("ShowFlightDetails", "Flight", new { FlightID = flightID });
                }

                if (flight.Tickets.Count() >= flight.Plane.Capacity)
                {
                    //SHOULD NOT PURCHASE
                    return RedirectToAction("ShowFlightDetails", "Flight", new { FlightID = flightID });
                }

                string appUserID = this.iAppUserRepo.GetAppUserID();
               
                //Create ticket
                Ticket ticket = new Ticket(flightID, appUserID, flight.Price);
                int ticketID = this.iTicketRepo.AddTicket(ticket);
                if (ticketID == 0)
                {
                    throw new Exception("This ticket was not added to the database. An error occurred.");
                }
                else
                {
                    ViewData["TicketID"] = ticketID;
                    //RETURN TICKETID
                }
                return View("PurchaseTicketConfirmation");
            }
            catch (Exception ex)
            {
                //RETURN ERROR
                return RedirectToAction("ShowFlightDetails", "Flight", new {FlightID = flightID});
            }
        }
    }
}
