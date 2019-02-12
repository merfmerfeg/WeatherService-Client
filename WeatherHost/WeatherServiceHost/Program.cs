using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using WeatherService;

namespace WeatherServiceHost
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var host = new ServiceHost(typeof(WeatherService.WeatherService) ))
            {
                host.Open();
                Console.WriteLine("Сервис запущен...");

                //Записывать данные в БД раз в 5 минут
                DbRecorder dbRecorder = new DbRecorder(300000);
                dbRecorder.RecordInDb += (WeatherItem wItem) => 
                    Console.WriteLine($"Запись в БД: {wItem}");


                Console.ReadLine();
            }
        }
    }
}
