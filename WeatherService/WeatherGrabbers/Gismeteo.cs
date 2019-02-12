using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    class Gismeteo : IWeatherGrabber
    {
        GismeteoGrabber.GismeteoGrabber _grabber;

        public Gismeteo()
        {
            _grabber = new GismeteoGrabber.GismeteoGrabber();
        }

        public List<WeatherItem> GetData()
        {
            var query = from item in _grabber.GetData()
                        select new WeatherItem()
                        {
                            City = item.City,
                            Cloudiness = (Cloudiness)item.Cloudiness,
                            Date = item.Date,
                            Humidity = item.Humidity,
                            Precipitation = (Precipitation)item.Precipitation,
                            Pressure = item.Pressure,
                            Temperature = item.Temperature,
                            TemperatureFeel = item.TemperatureFeel,
                            Wind = item.Wind
                        };

            return query.ToList();
        }

        public string[] GetMainCityList()
        {
            return _grabber.GetMainCityList();
        }
    }
}
