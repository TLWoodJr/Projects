using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Model used in Survey: Index, Controller: Survey
    /// </summary>
    public class FavoriteParkViewModel
    {
        /// <summary>
        /// Park code to get correct image file
        /// </summary>
        public string ParkCode { get; set; }

        /// <summary>
        /// Number of survey votes each park has received 
        /// </summary>
        public int Votes { get; set; }

        /// <summary>
        /// Park name to be displayed
        /// </summary>
        public string ParkName { get; set; }
    }
}