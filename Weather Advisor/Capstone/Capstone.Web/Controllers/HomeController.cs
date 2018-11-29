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
    /// Controller for website home page. Actions display park information.
    /// </summary>
    public class HomeController : Controller
    {
        IParksDAL _dal = null;

        public enum TempPreference
        {
            Fahrenheit,
            Celsius
        }
        /// <summary>
        /// Injecting interface into the HomeController constructor
        /// </summary>
        /// <param name="dal"></param>
        public HomeController(IParksDAL dal)
        {
            _dal = dal;
        }

        /// <summary>
        /// List of parks displaying associated park pictures, the park name, and the park description.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Index()
        {
            List<ParksHomeViewModel> parks = _dal.GetParks();
            return View(parks);
        }

        /// <summary>
        /// Uses <paramref name="parkCode"/> to retrieve all database data on that park, as well as the five day forecast for that park.
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Details(string parkCode)
        {
            TempData["code"] = parkCode;
            DetailViewModel model = _dal.GetDetails(parkCode);
            
            return View("Details", model);
        }

        /// <summary>
        /// User selects <paramref name="preference"/> (Fahrenheit/Celsius) on details page, saves their preference to Session, and is returned to the details page for the park they were viewing.
        /// </summary>
        /// <param name="preference"></param>
        /// <returns></returns>
        public ActionResult SetTempPreference(string preference/*TempPreference preference = TempPreference.Fahrenheit*/)
        {
            Session["TemperaturePreference"] = preference;
            return Redirect(Url.RouteUrl(new { controller = "Home", action = "Details", parkCode = TempData["code"] })+"#weathersection");
        }
    }
}