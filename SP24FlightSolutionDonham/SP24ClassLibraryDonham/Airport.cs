using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Airport
    {
        [Key]
        public int AirportID { get; set; }
        [Required]
        public string AirportName { get; set; }
        public string? Abbreviation { get; set; }
        public int NumberOfGates { get; set; }

        //LAT LONG
        public decimal? Latitude { get; set; }
        public decimal? Longitude { get; set; }

        [NotMapped]
        public List<Flight> Flights { get; set; } = new List<Flight>();

        public Airport(string airportName, string? abbreviation = null, int numberOfGates = 0, decimal? lat = null, decimal? lon = null)
        {
            this.AirportName = airportName;
            this.Abbreviation = abbreviation;
            this.NumberOfGates = numberOfGates;
            this.Latitude = lat;
            this.Longitude = lon;
        }

        public Airport()
        {
            
        }
    }
}
