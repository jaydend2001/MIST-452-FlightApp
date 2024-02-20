using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Plane
    {
        [Key]
        public int PlaneID { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Capacity { get; set; }

        //Foreign Key (Integer) - Migrate between tables in DB
        [Required]
        public int AirlineID { get; set; }

        //Object Connections - Navigate between classes
        //Entire Airline 1, Southwest, LUV
        [ForeignKey(nameof(AirlineID))]
        public Airline Airline { get; set; }

        public List<Flight> Flights { get; set; } = new List<Flight>();

        public Plane(string model, int capacity, int airlineID)
        {
            this.Model = model;
            this.Capacity = capacity;
            this.AirlineID = airlineID;
        }
        public Plane()
        {
                
        }
    }
}
