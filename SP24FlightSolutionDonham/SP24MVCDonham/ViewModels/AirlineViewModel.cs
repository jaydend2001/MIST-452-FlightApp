using System.ComponentModel.DataAnnotations;

namespace SP24MVCDonham.ViewModels
{
    public class AirlineViewModel
    {
        [Required]
        public string AirlineName {  get; set; }
        public string? StockTicker { get; set; }
        public IFormFile? Logo { get; set; }
    }
}
