using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WeatherService
{
    [ServiceContract]
    public interface IWeatherService
    {
        //Получить последние записанные данные по конкретному городу
        [OperationContract]
        WeatherItem GetWeatherDataLast(string city);

        //Получить данные по дате и по конкретному городу
        [OperationContract]
        List<WeatherItem> GetWeatherDataByDate(string city, DateTime dateStart, DateTime dateEnd);

        //Получить список городов с главной страницы Gismeteo
        [OperationContract]
        string[] GetMainCityList();

    }
}
