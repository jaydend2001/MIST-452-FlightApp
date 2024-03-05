using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Flight
    {
        [Key]
        public int FlightID { get; set; }
        [Required]
        public DateTime DepartureDateTime { get; set; }
        [Required]
        public DateTime EstimatedArrivalDateTime { get; set; }

        public TimeSpan Duration
        {
            get
            {
                return this.EstimatedArrivalDateTime - this.DepartureDateTime;
            }
        }

        [Required]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }

        [Required]
        public FlightStatus FlightStatus { get; set; } = FlightStatus.Planned;

        //Foreign Key
        [Required]
        public int PlaneID { get; set; }
        [ForeignKey(nameof(PlaneID))]
        public Plane Plane { get; set; }

        //DepartureAirport
        [Required]
        public int DepartureAirportID { get; set; }
        [ForeignKey(nameof(DepartureAirportID))]
        public Airport DepartureAirport { get; set; }

        //Arrival Airport
        [Required]
        public int ArrivalAirportID { get; set; }
        [ForeignKey(nameof(ArrivalAirportID))]
        public Airport ArrivalAirport { get; set; }

        //Parameterized
        public Flight(DateTime departure, DateTime arrival, decimal price, int departureAirportID, int arrivalAirportID, int planeID)
        {
            this.DepartureDateTime = departure;
            this.EstimatedArrivalDateTime = arrival;
            this.Price = price;
            this.DepartureAirportID = departureAirportID;
            this.ArrivalAirportID = arrivalAirportID;
            this.PlaneID = planeID;
        }
        //Default
        public Flight()
        {
            this.FlightStatus = FlightStatus.Planned;
        }

    }//class

    public enum FlightStatus
    {
        Planned,
        Delayed,
        Departed,
        Arrived,
        Cancelled
    }

}//namespace
