using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Timers;

namespace WeatherService
{
    public class WeatherService : IWeatherService
    {
        private DataBasesWork.DataBaseController _dbController;
        private IWeatherGrabber _weatherGrabber;

        public WeatherService()
        {
            //класс для управления БД
            _dbController = new DataBasesWork.DataBaseController();
            //класс для управлением парсером погоды
            _weatherGrabber = new Gismeteo();
        }

        public string[] GetMainCityList()
        {
            return _weatherGrabber.GetMainCityList();
        }

        public List<WeatherItem> GetWeatherDataByDate(string city, DateTime dateStart, DateTime dateEnd)
        {
            List<WeatherItem> returnVal = new List<WeatherItem>();

            foreach (var item in _dbController.GetDataByDate(dateStart, dateEnd, city))
                returnVal.Add(new WeatherItem(item));

            return returnVal;
        }

        public WeatherItem GetWeatherDataLast(string city)
        {
            return new WeatherItem(_dbController.GetDataLast(city));
        }
    }
}
