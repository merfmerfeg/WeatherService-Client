using Microsoft.VisualStudio.TestTools.UnitTesting;
using DataBasesWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Tests
{
    [TestClass()]
    public class DataBaseControllerTests
    {
        DataBaseController _db;

        [TestInitialize()]
        public void Init()
        {
            _db = new DataBaseController();
        }

        [TestMethod()]
        public void GetDataLastTest()
        {
            _db.SetData(new DataBaseItemTest
            {
                City = "Test",
                Cloudiness = 100,
                Date = DateTime.Now.AddMinutes(-2),
                Humidity = 100,
                Precipitation = 100,
                Pressure = 60,
                Temperature = -10.5,
                TemperatureFeel = -12,
                Wind = 5
            });

            Assert.AreEqual(-10.5, _db.GetDataLast("Test").Temperature);
        }

        [TestMethod()]
        public void GetDataByDateTest()
        {
            _db.SetData(new DataBaseItemTest
            {
                City = "Test",
                Cloudiness = 100,
                Date = DateTime.Now,
                Humidity = 100,
                Precipitation = 100,
                Pressure = 60,
                Temperature = -10.5,
                TemperatureFeel = -12.1,
                Wind = 5
            });

            _db.SetData(new DataBaseItemTest
            {
                City = "Test",
                Cloudiness = 100,
                Date = DateTime.Now,
                Humidity = 100,
                Precipitation = 100,
                Pressure = 60,
                Temperature = -11.5,
                TemperatureFeel = -12,
                Wind = 5
            });

            var result = _db.GetDataByDate(DateTime.Now.AddSeconds(-1), DateTime.Now.AddSeconds(1), "Test");

            Assert.AreEqual(-12.1, result.First().TemperatureFeel);
        }
    }
}