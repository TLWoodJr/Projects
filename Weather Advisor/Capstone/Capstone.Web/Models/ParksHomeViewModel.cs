using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Model designed for use in Action: Index Controller: Home
    /// </summary>
    public class ParksHomeViewModel
    {
        /// <summary>
        /// Retrieved from database, used for associated .jpg file
        /// </summary>
        public string ParkCode { get; set; }
        /// <summary>
        /// Name of the park
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// Description of the park
        /// </summary>
        public string Description { get; set; }
    }
}