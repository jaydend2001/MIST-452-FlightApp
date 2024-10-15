using System.ComponentModel.DataAnnotations;

namespace SP24MVCDonham.ViewModels
{
    public class FlightViewModel
    {
        [Required]
        public int? DepartureAirportID { get; set; }

        [Required]
        public int? ArrivalAirportID { get; set; }

        [Required]
        public int? PlaneID { get; set; }

        [Required]
        public DateTime? DepartureDateTime { get; set; }

        [Required]
        public DateTime? ArrivalDateTime { get; set;}

        [Required]
        [DataType(DataType.Currency)]
        public decimal? Price { get; set; }

        public int? FlightID { get; set; }
    }
}
