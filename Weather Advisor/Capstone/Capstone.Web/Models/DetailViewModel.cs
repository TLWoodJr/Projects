using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Model used in Details: Index, Controller: Home
    /// </summary>
    public class DetailViewModel
    {
        /// <summary>
        /// ParksModel object.
        /// </summary>
        public ParksModel Park { get; set; }
        /// <summary>
        /// List of WeatherDayModel objects assciated with Park ParkCode.
        /// </summary>
        public List<WeatherDayModel> FiveDayForecast {get; set;}
    }
}