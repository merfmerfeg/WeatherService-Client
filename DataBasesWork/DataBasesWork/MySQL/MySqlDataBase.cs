using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LinqToDB;

namespace DataBasesWork
{
    class MySqlDataBase : IDatabaseItemCollection
    {
        private WeatherDb _appDb;
        private int _id;

        public MySqlDataBase()
        {
            if (_appDb == null)
            {
                _appDb = new WeatherDb();
                _id = _appDb.WeatherTable.OrderByDescending(o => o.Id).First().Id;
            }
        }

        public List<IDatabaseItem> GetDataByDate(DateTime dateStart, DateTime dateEnd, string cityName)
        {
            List<IDatabaseItem> returnVal = new List<IDatabaseItem>();

            var query = from item in _appDb.WeatherTable
                        where item.City == cityName && item.Date >= dateStart && item.Date <= dateEnd
                        select item;

            returnVal.AddRange(query.ToList());

            return returnVal;
        }

        public IDatabaseItem GetDataLast(string cityName)
        {
            var query = from item in _appDb.WeatherTable
                   where item.City == cityName
                   select item;

            return query.ToList().LastOrDefault();
        }

        public void SetData(List<IDatabaseItem> dbItems)
        {
            using (var db = new WeatherDb())
            {
                foreach (var dbItem in dbItems)
                {
                    db.Insert(new MySqlDataItem
                    {
                        Id = ++_id,
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
                }
            }
        }

        public void SetData(IDatabaseItem dbItem)
        {
            _appDb.Insert(new MySqlDataItem
            {
                Id = ++_id,
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
        }
    }
}
