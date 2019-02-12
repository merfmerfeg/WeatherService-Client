using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public interface IDatabaseItem
    {
        DateTime Date { get; set; }
        double Temperature { get; set; }
        double TemperatureFeel { get; set; }
        int Wind { get; set; }
        int Pressure { get; set; }
        int Humidity { get; set; }
        int Cloudiness { get; set; }
        int Precipitation { get; set; }
        string City { get; set; }
    }

    public interface IDatabaseItemCollection
    {
        void SetData(IDatabaseItem dbItem);
        IDatabaseItem GetDataLast(string cityName);
        IList<IDatabaseItem> GetDataByDate(DateTime dateStart, DateTime dateEnd, string cityName);
    }
}
