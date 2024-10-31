using Microsoft.AspNetCore.Mvc;
using System.Runtime.Serialization;

namespace SP24MVCDonham.Models
{
    public class Chart
    {
        //DataContract for Serializing Data - required to serve in JSON format
        [DataContract]
        public class DataPoint
        {
            public DataPoint(string label, double? y, int? id)
            {
                this.Label = label;
                this.Y = y;
                this.ID = id;
            }
            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "label")]
            public string Label = "";
            //Explicitly setting the name to be used while serializing to JSON.
            [DataMember(Name = "y")]
            public Nullable<double> Y = null;
            [DataMember(Name = "id")]
            public Nullable<int> ID = null;
        }
    }
}
