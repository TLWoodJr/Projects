using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Capstone.Web.Models
{
    /// <summary>
    /// Model that holds data for submission to NPGeeks database survey_reponse table.
    /// </summary>
    public class SurveyModel
    {
        /// <summary>
        /// List of states for dropdown in Survey Submit form.
        /// </summary>
        public static List<string> States = new List<string>()
        {
            "AL","AK","AZ","AR","CA","CO","CT", "DE","FL","GA","HI",
            "ID","IL","IN","IA","KS","KY","LA","ME","MD","MA","MI",
            "MN","MS","MO","MT","NE","NV","NH","NJ","NM","NY","NC",
            "ND","OH","OK","OR","PA","RI","SC","SD","TN","TX","UT",
            "VT","VA", "WA","WV","WI","WY"
        };

        
        [Required (ErrorMessage = "Park name is required")]
        public string ParkName { get; set; }

        [Required(ErrorMessage = "Email address is required")]
        [EmailAddress]
        public string Email { get; set; }

        [Required(ErrorMessage = "State of residence is required")]
        public string ResidenceState { get; set; }

        [Required(ErrorMessage = "Activity level is required")]
        public string ActivityLevel { get; set; }
    }
}