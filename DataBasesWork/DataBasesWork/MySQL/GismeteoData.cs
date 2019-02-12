using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoGrabber
{
    public class GismeteoData
    {
        public DateTime Date {get; set;}
        public double Temperature { get; set; }
        public double TemperatureFeel { get; set; }
        public int Wind  { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public Cloudiness Cloudiness { get; set; }
        public Precipitation Precipitation { get; set; }
        public string City { get; set; }
    }
}
