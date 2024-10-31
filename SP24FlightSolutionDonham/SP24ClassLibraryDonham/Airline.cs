using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Airline
    {
        [Key]
        public int AirlineID { get; set; }
        [Required]
        public string AirlineName { get; set; }
        public string? StockTicker { get; set; }

        //MANY
        //Not saved in DB
        //[NotMapped]
        public List<Plane> Planes { get; set; }

        //Parameterized Constructor
        public Airline(string airlineName, string? stockTicker = null)
        {
            this.AirlineName = airlineName;
            this.StockTicker = stockTicker;
            this.Planes = new List<Plane>();
        }

        //public Airline(string airlineName, string stockTicker)
        //{
            
        //}

        //Default Constructor - Needed Entity Framework (EF)
        //EF is ORM (Object Relational Mapper)
        public Airline() { }
    }
}
