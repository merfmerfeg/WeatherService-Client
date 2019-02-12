using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace DataBase
{
    class MySqlDataBase : IDatabaseItemCollection
    {
        private static ApplicationContext _appDb;

        public MySqlDataBase()
        {
            if (_appDb == null)
            {
                _appDb = new ApplicationContext();
                _appDb.Database.EnsureCreated();
            }
        }

        public IList<IDatabaseItem> GetDataByDate(DateTime dateStart, DateTime dateEnd, string cityName)
        {
            IList<IDatabaseItem> returnValue;

            returnValue = (IList<IDatabaseItem>)_appDb.WeatherData.Select(
                s => 
                s.Date >= dateStart && 
                s.Date <= dateEnd && 
                s.City == cityName
                ).ToList();

            return returnValue;
        }

        public IDatabaseItem GetDataLast(string cityName)
        {
            return _appDb.WeatherData.LastOrDefault(w => w.City == cityName);
        }

        public void SetData(IDatabaseItem dbItem)
        {
            _appDb.WeatherData.Add(new MySqlDataItem
            {
                City = dbItem.City,
                Cloudiness = dbItem.Cloudiness,
                Date = dbItem.Date,
                Humidity = dbItem.Humidity,
                Precipitation = dbItem.Precipitation,
                Pressure = dbItem.Pressure,
                Temperature = dbItem.Temperature,
                TemperatureFeel = dbItem.TemperatureFeel,
                Wind = dbItem.Wind
            });
            _appDb.SaveChanges();
        }
    }
}
