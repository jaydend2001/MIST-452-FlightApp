using SP24ClassLibraryDonham;

namespace SP24MVCDonham.ViewModels
{
    public class SearchFlightsViewModel
    {
        //Search Criteria
        //Inputs
        public int? DepartureAirportID { get; set; }
        public int? ArrivalAirportID { get; set; }
        public int? AirlineID { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? AirlineName { get; set; }
        public bool AvailableFlights { get; set; }
        public FlightStatus? FlightStatus { get; set; }

        //Result
        public List<Flight> SearchResult {  get; set; }
    }
}
