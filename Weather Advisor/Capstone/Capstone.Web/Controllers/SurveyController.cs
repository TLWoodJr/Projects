using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Capstone.Web.DAL;
using Capstone.Web.Models;

namespace Capstone.Web.Controllers
{
    /// <summary>
    /// Displays surveys saved to database, and retrives user input for Survey Models.
    /// </summary>
    public class SurveyController : Controller
    {
        ISurveyDAL _dal = null;
        
        /// <summary>
        /// Injecting interface into the SurveyController constructor
        /// </summary>
        /// <param name="dal"></param>
        public SurveyController(ISurveyDAL dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// Retrieves list of parks with votes saved to database. Displays associated park image, name, and number of votes.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<FavoriteParkViewModel> parks = _dal.GetFavoriteParks();
            return View(parks);
        }

        /// <summary>
        /// Requests input from user to fill a SurveyModel.
        /// </summary>
        /// <returns>View "Get Submit". Form directs to "Post Submit".</returns>
        [HttpGet]
        public ActionResult Submit()
        {
            List<string> parks = _dal.GetParkNames();
            return View(parks);
        }

        /// <summary>
        /// Saves a SurveyModel to database. Redirects to Index in Survey controller.
        /// </summary>
        /// <param name="survey"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Submit(SurveyModel survey)
        {
            ActionResult result = null;

            if (!ModelState.IsValid)
            {
                result = Submit();
            }
            else
            {
                bool success = _dal.AddSurvey(survey);
                if (!success)
                {
                    result = Submit();
                }
                else
                {
                    result = RedirectToAction("Index");
                }
            }

            return result;
        }
    }
}