using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Model for displaying weather details for five day forecase in Action: Details, Controller: Home
    /// </summary>
    public class WeatherDayModel
    {
        public string ParkCode { get; set; }
        public int FiveDayForecastValue { get; set; }
        /// <summary>
        /// Temperature in Fahrenheit read in from NPGeek database.
        /// </summary>
        public double Low { get; set; }
        /// <summary>
        /// Temperature in Fahrenheit read in from NPGeek database.
        /// </summary>
        public double High { get; set; }
        /// <summary>
        /// Derived property for calculating Low converted from Fahrenheit to Celsius
        /// </summary>
        public double CelsiusLow
        {
            get
            {
                return (Low-32) * 5 / 9;
            }
        }
        /// <summary>
        /// Derived property for calculating High converted from Fahrenheit to Celsius
        /// </summary>
        public double CelsiusHigh
        {
            get
            {
                return (High - 32) * 5 / 9;
            }
        }
        /// <summary>
        /// Read in from NPGeek database
        /// </summary>
        public string Forecast { get; set; }

        /// <summary>
        /// Uses Low, High, and Forecast to display advisory to user.
        /// </summary>
        /// <returns>String advisory</returns>
        public string GetAdvisory()
        {
            string advisory = "";
            if(Forecast == "snow")
            {
                advisory = advisory + "Pack your snowshoes!";
            }
            if(Forecast == "rain")
            {
                advisory = advisory + "Pack raingear and waterproof shoes!";
            }
            if(Forecast == "thunderstorms")
            {
                advisory = advisory + "Seek shelter and avoid exposed ridges!";
            }
            if(Forecast == "sunny")
            {
                advisory = advisory + "Pack sun block!";

            }
            if (High > 75)
            {
                advisory = advisory + " Bring an extra gallon of water!";
            }
            if (High - Low > 20)
            {
                advisory = advisory + " Make sure to pack breathable layers!";
            }
            if (Low < 20)
            {
                advisory = advisory + " Watch out for frigid temperatures!";
            }

            advisory = advisory.TrimStart();

            return advisory;
        }


        /// <summary>
        /// Method used to decide which Low temperature property to use based on <paramref name="pref"/> (Fahrenheit/Celsius).
        /// </summary>
        /// <param name="pref"></param>
        /// <returns>Low or CelsiusLow formatted as a string with no decimal denoted F or C.</returns>
        public string DisplayLow(string pref)
        {
            if (pref == "Fahrenheit" || pref == null)
            {
                return Low.ToString("N0")+" F";
            }
            else
            {
                return CelsiusLow.ToString("N0") + " C";
            }
        }

        /// <summary>
        /// Method used to decide which High temperature property to use based on <paramref name="pref"/> (Fahrenheit/Celsius).
        /// </summary>
        /// <param name="pref"></param>
        /// <returns>Low or CelsiusLow formatted as a string with no decimal denoted F or C.</returns>
        public string DisplayHigh(string pref)
        {
            if (pref == "Fahrenheit" || pref == null)
            {
                return High.ToString("N0") + " F";
            }
            else
            {
                return CelsiusHigh.ToString("N0") + " C";
            }
        }
    }
}