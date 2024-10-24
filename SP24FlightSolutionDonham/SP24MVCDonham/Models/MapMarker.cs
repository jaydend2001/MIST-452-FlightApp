using System.Drawing;

namespace SP24MVCDonham.Models
{
    public class MapMarker
    {
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public string Label { get; set; }
        public int ID { get; set; }
        public string Color { get; set; } = "Blue";

        public MapMarker(int id, string label, decimal latitude, decimal longitude, string color = "Blue")
        {
            this.ID = id;
            this.Label = label;
            this.Latitude = latitude;
            this.Longitude = longitude;
            this.Color = color;
        }

        public MapMarker()
        {
            
        }
    }
}
