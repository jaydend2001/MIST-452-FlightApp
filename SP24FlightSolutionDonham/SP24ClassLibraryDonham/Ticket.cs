using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP24ClassLibraryDonham
{
    public class Ticket
    {
        [Key]
        public int TicketID { get; set; }

        [Required]
        public DateTime PurchaseDateTime { get; set; } = DateTime.Now;

        [Required]
        [DataType(DataType.Currency)]
        public decimal PurchasePrice { get; set; }

        [Required]
        public int FlightID { get; set; }

        [ForeignKey(nameof(FlightID))]
        public Flight Flight { get; set; }

        [Required]
        public string AppUserID { get; set; }

        [ForeignKey(nameof(AppUserID))]
        public AppUser AppUser { get; set; }

        public Ticket(int flightID, string appUserID, decimal price)
        {
            this.FlightID = flightID;
            this.AppUserID = appUserID;
            this.PurchasePrice = price;
        }

        public Ticket()
        {
            
        }
    }
}
