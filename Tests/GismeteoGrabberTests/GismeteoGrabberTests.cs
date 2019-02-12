using Microsoft.VisualStudio.TestTools.UnitTesting;
using GismeteoGrabber;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GismeteoGrabber.Tests
{
    [TestClass()]
    public class GismeteoGrabberTests
    {
        [TestMethod()]
        public void GetSityListTest()
        {
            GismeteoGrabber gg = new GismeteoGrabber();
            var data = gg.GetData();

            Assert.AreEqual(24, data.Count);
        }
    }
}