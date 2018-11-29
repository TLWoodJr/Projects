using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Capstone.Web.Models;

namespace Capstone.Web.Tests.Models
{
    [TestClass]
    public class WeatherDayModelTests
    {
        WeatherDayModel weather = new WeatherDayModel();

        [TestInitialize]
        public void Initialize()
        {
            weather = new WeatherDayModel();
        }

        [TestMethod]
        public void CelsiusLowTest()
        {
            weather.Low = 32;
            Assert.AreEqual(0, weather.CelsiusLow, "C should be 0.");
        }

        [TestMethod]
        public void CelsiusHighTest()
        {
            weather.High = 212;
            Assert.AreEqual(100, weather.CelsiusHigh, "C should be 100.");
        }

        [TestMethod]
        public void GetAdvisoryTest()
        {
            weather.Forecast = "snow";
            weather.Low = 32;
            Assert.AreEqual("Pack your snowshoes!", weather.GetAdvisory(), "Response should be 'Pack your snowshoes!'");
        }

        [TestMethod]
        public void GetAdvisoryTest2()
        {
            weather.Forecast = "sunny";
            weather.High = 80;
            weather.Low = 70;
            Assert.AreEqual("Pack sun block! Bring an extra gallon of water!", weather.GetAdvisory(), "Response should be 'Pack sun block! Bring an extra gallon of water!'");
        }

        [TestMethod]
        public void DisplayLowFTest()
        {
            string pref = "Fahrenheit";
            weather.Low = 32;
            Assert.AreEqual("32 F", weather.DisplayLow(pref), "Should show '32 F'");
        }

        [TestMethod]
        public void DisplayLowCTest()
        {
            string pref = "Celsius";
            weather.Low = 32;
            Assert.AreEqual("0 C", weather.DisplayLow(pref), "Should show '0 C'");
        }

        [TestMethod]
        public void DisplayHighFTest()
        {
            string pref = "Fahrenheit";
            weather.High = 32;
            Assert.AreEqual("32 F", weather.DisplayHigh(pref), "Should show '32 F'");
        }

        [TestMethod]
        public void DisplayHighCTest()
        {
            string pref = "Celsius";
            weather.High = 32;
            Assert.AreEqual("0 C", weather.DisplayHigh(pref), "Should show '0 C'");
        }
    }
}
