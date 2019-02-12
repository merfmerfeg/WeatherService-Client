using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using DataBasesWork;

namespace WeatherService
{
    public class DbRecorder
    {
        private IWeatherGrabber _weatherGrabber;
        private DataBaseController _dbController;
        private Timer _timer;

        public DbRecorder(int intervalDbRecord)
        {
            //класс для управления БД
            _dbController = new DataBaseController();
            //класс парсера gismeteo
            _weatherGrabber = new Gismeteo();
            //класс таймера для записи в БД
            _timer = new Timer(); 

            //настраиваем таймер
            _timer.Interval = intervalDbRecord;
            _timer.Elapsed += (object sender, ElapsedEventArgs e) =>
            {
                foreach (var item in _weatherGrabber.GetData())
                {
                    _dbController.SetData(item);
                    RecordInDb?.Invoke(item);
                }
            };

            _timer.Start();
        }

        public event Action<WeatherItem> RecordInDb;
    }
}
