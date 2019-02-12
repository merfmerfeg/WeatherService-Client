using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataBasesWork;

namespace WeatherService
{
    public class WeatherItem : IDatabaseItem
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double TemperatureFeel { get; set; }
        public int Wind { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public Cloudiness Cloudiness { get; set; }
        public Precipitation Precipitation { get; set; }
        public string City { get; set; }
        int IDatabaseItem.Cloudiness { get => (int)this.Cloudiness; set => this.Cloudiness = (Cloudiness)value; }
        int IDatabaseItem.Precipitation { get => (int)this.Precipitation; set => this.Precipitation = (Precipitation)value; }

        public WeatherItem() { }
        public WeatherItem(IDatabaseItem item)
        {
            Date = item.Date;
            Temperature = item.Temperature;
            TemperatureFeel = item.TemperatureFeel;
            Wind = item.Wind;
            Pressure = item.Pressure;
            Humidity = item.Humidity;
            Cloudiness = (Cloudiness)item.Cloudiness;
            Precipitation = (Precipitation)item.Precipitation;
            City = item.City;
        }

        public override string ToString()
        {
            return $"Date = {Date}, City = {City}, Cloudiness = {Cloudiness}, Precipitation = {Precipitation}, Temperature = {Temperature}";
        }
    }
}
