using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherService
{
    public enum Cloudiness
    {
        туман = -1,
        ясно = 0, 
        малооблачно = 1,
        облачно = 2,
        пасмурно = 3
    }

    public enum Precipitation
    {
        смешанные = 3,
        дождь = 4,
        ливень = 5,
        снег = 6,
        снегопад = 7,
        гроза = 8,
        нет_данных = 9,
        без_осадков = 10
    }
}
