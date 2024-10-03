using SP24ClassLibraryDonham;
using SP24MVCDonham.Data;

namespace SP24MVCDonham.Models
{
    public class TicketRepo : ITicketRepo
    {
        private ApplicationDbContext database;

        public TicketRepo(ApplicationDbContext dbContext)
        {
            this.database = dbContext;
        }

        public int AddTicket(Ticket ticket)
        {
            this.database.Tickets.Add(ticket);
            this.database.SaveChanges();
            return ticket.TicketID;
        }
    }
}
