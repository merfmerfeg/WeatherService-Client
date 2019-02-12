using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GismeteoGrabber
{
    public class GismeteoGrabber
    {
        private const string GISMETEO_URL = "https://www.gismeteo.ru";
        private const string GISMETEO_INFORM_URL = "http://informer.gismeteo.ru/xml/";

        public List<GismeteoData> GetData()
        {
            List<GismeteoData> resultList = new List<GismeteoData>();

            Parallel.ForEach(GetCityList(), city =>
            {
                var cityResult = ParseCityData(city.Key);

                lock(resultList)
                {
                    resultList.Add(cityResult);
                }
            });

            return resultList;
        }

        public string[] GetMainCityList()
        {
            return GetCityList().Select(s => s.Key).ToArray();
        }

        private List<KeyValuePair<string, string>> GetCityList()
        {
            List<KeyValuePair<string, string>> resultList = new List<KeyValuePair<string, string>>();

            string html = GetHttpPage(GISMETEO_URL);

            //Выбираем текст с описанием городов и их параметров с главной страницы 
            var posFirst = html.IndexOf("<!-- City frame -->");
            var posLast = html.IndexOf("<!-- End City frame -->");
            string cityParams = html.Substring(posFirst, posLast - posFirst);

            //Парсим выбранный текст в xml 
            var node = XElement.Parse(cityParams);

            //Выбираем нужные параметры
            foreach (var elem in node.Elements())
            {
                XAttribute nameCity = elem.Attribute("data-name");
                XAttribute urlCity = elem.Attribute("data-url");

                resultList.Add(new KeyValuePair<string, string>(nameCity.Value, urlCity.Value));
            }

            return resultList;
        }

        /* xml format
        TOWN - информация о пункте прогнозирования:
            index -	уникальный код города
            sname	- закодированное название города
            latitude -	широта в целых градусах
            longitude -	долгота в целых градусах
        FORECAST - информация о сроке прогнозирования:
            day, month, year -	дата, на которую составлен прогноз в данном блоке
            hour -	местное время, на которое составлен прогноз
            tod -	время суток, для которого составлен прогноз: 0 - ночь 1 - утро, 2 - день, 3 - вечер
            weekday -	день недели, 1 - воскресенье, 2 - понедельник, и т.д.
            predict -	заблаговременность прогноза в часах
        PHENOMENA - атмосферные явления:
            cloudiness -	облачность по градациям: -1 - туман, 0 - ясно, 1 - малооблачно, 2 - облачно, 3 - пасмурно
            precipitation -	тип осадков: 3 - смешанные, 4 - дождь, 5 - ливень, 6,7 – снег, 8 - гроза, 9 - нет данных, 10 - без осадков
            rpower -	интенсивность осадков, если они есть. 0 - возможен дождь/снег, 1 - дождь/снег
            spower -	вероятность грозы, если прогнозируется: 0 - возможна гроза, 1 - гроза
        PRESSURE -	атмосферное давление, в мм.рт.ст.
        TEMPERATURE - температура воздуха, в градусах Цельсия
        WIND -	приземный ветер
            min, max -	минимальное и максимальное значения средней скорости ветра, без порывов (м/с)
            direction - направление ветра в румбах, 0 - северный, 1 - северо-восточный, и т.д.
        RELWET -	относительная влажность воздуха, в %
        HEAT - комфорт - температура воздуха по ощущению одетого по сезону человека, выходящего на улицу
        */
        private GismeteoData ParseCityData(string cityName)
        {
            //Получаем id выбранного города
            string cityId = $"{GetCityIndex(cityName)}_1.xml";

            //Парсим полученные xml данные
            var node = XElement.Parse(GetHttpPage(GISMETEO_INFORM_URL+cityId));
            var xe = node.Descendants("FORECAST").Last();

            return new GismeteoData
            {
                City = cityName,
                Date = DateTime.Now,
                Cloudiness = (Cloudiness)int.Parse(xe.Element("PHENOMENA").Attribute("cloudiness").Value),
                Precipitation = (Precipitation)int.Parse(xe.Element("PHENOMENA").Attribute("precipitation").Value),
                Temperature = double.Parse(xe.Element("TEMPERATURE").Attribute("max").Value),
                TemperatureFeel = double.Parse(xe.Element("HEAT").Attribute("max").Value),
                Pressure = int.Parse(xe.Element("PRESSURE").Attribute("max").Value),
                Wind = int.Parse(xe.Element("WIND").Attribute("max").Value),
                Humidity = int.Parse(xe.Element("RELWET").Attribute("max").Value)
            };
        }

        private string GetCityIndex(string cityName)
        {
            //Получение ID города из xml config
            string configFileName = $"{Environment.CurrentDirectory}//CityList.xml";
            var doc = XDocument.Load(configFileName);

            var element = doc.Descendants("City").FirstOrDefault(f => f.Attribute("Name").Value == cityName);

            return element.Attribute("Id").Value;
        }

        private string GetHttpPage(string url)
        {
            //Запрос HTTP
            HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
            HttpWebResponse httpWebResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            StreamReader streamReader = new StreamReader(httpWebResponse.GetResponseStream());
            return streamReader.ReadToEnd();
        }
    }
}
