using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    interface IWeatherGrabber
    {
        List<WeatherItem> GetData();
        string[] GetMainCityList();
    }
}
