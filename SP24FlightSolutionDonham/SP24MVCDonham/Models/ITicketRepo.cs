using SP24ClassLibraryDonham;

namespace SP24MVCDonham.Models
{
    public interface ITicketRepo
    {
        public int AddTicket(Ticket ticket);
    }
}
