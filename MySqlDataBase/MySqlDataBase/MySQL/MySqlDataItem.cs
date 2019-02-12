using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class MySqlDataItem : IDatabaseItem 
    {
        public DateTime Date { get; set; }
        public double Temperature { get; set; }
        public double TemperatureFeel { get; set; }
        public int Wind { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public int Cloudiness { get; set; }
        public int Precipitation { get; set; }
        public string City { get; set; }
    }


}
