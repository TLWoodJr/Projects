using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Data.SqlClient;
using Capstone.Web.Models;
using Capstone.Web.DAL;

namespace Capstone.Web.Tests.DAL
{
    [TestClass]
    public class ParksSqlDALTests
    {
        private TransactionScope _tran;
        private string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = NPGeek; Integrated Security = True";
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        [TestInitialize]
        public void Initialize()
        {
            _tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                string insertDetails = "INSERT INTO park (parkCode, parkName, state, acreage, elevationInFeet, milesOfTrail, numberOfCampsites, climate, yearFounded, annualVisitorCount, inspirationalQuote, inspirationalQuoteSource, parkDescription, entryFee, numberOfAnimalSpecies) " +
                                                 "VALUES ('TEST','TEST', 'TEST', 1, 1, 1, 1, 'TEST', 2018, 1, 'TEST', 'TEST', 'TEST', 1, 1);";
                string insertWeather = "INSERT INTO weather (parkCode, fiveDayForecastValue, low, high, forecast) " +
                                       "VALUES ('TEST', 1, 1, 1, 'TEST');";


                cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = insertDetails +insertWeather;
                cmd.ExecuteNonQuery();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        //Methods to test:
        //List<ParksHomeViewModel> GetParks();
        //DetailViewModel GetDetails(string parkCode);
        //GetFiveDayForecast(string parkCode);


        [TestMethod]
        public void GetFiveDayForecastTest()
        {
            IParksDAL dal = new ParksSqlDAL(connectionString);
            DetailViewModel model = dal.GetDetails("TEST");

            Assert.AreEqual("TEST", model.FiveDayForecast[0].Forecast);
            Assert.AreEqual(1, model.FiveDayForecast[0].FiveDayForecastValue);
            Assert.AreEqual(1, model.FiveDayForecast[0].Low);
            Assert.AreEqual(1, model.FiveDayForecast[0].High);
        }

        [TestMethod]
        public void GetParks()
        {
            IParksDAL dal = new ParksSqlDAL(connectionString);
            List<ParksHomeViewModel> parks = dal.GetParks();
            bool tag = false;
            ParksHomeViewModel test = new ParksHomeViewModel();
            foreach (var park in parks)
            {
                if(park.Name == "TEST")
                {
                    test = park;
                    tag = true;
                }
            }
            Assert.IsTrue(tag);
            Assert.AreEqual("TEST", test.ParkCode, "Park code");
            Assert.AreEqual("TEST", test.Description, "Description");
        }
            

        [TestMethod]
        public void GetDetailsTest()
        {
            IParksDAL dal = new ParksSqlDAL(connectionString);
            DetailViewModel model = dal.GetDetails("TEST");
            Assert.AreEqual("TEST", model.Park.ParkCode);
            Assert.AreEqual("TEST", model.Park.ParkName);
            Assert.AreEqual("TEST", model.Park.State);
            Assert.AreEqual("TEST", model.Park.Climate);
            Assert.AreEqual("TEST", model.Park.InspirationalQuote);
            Assert.AreEqual("TEST", model.Park.InspirationalQuoteSource);
            Assert.AreEqual("TEST", model.Park.ParkDescription);
            Assert.AreEqual("TEST", model.Park.ParkCode);

            Assert.AreEqual(1, model.Park.Acreage, "Acreage");
            Assert.AreEqual(1, model.Park.Elevation, "Elevation");
            Assert.AreEqual(1, model.Park.MilesOfTrail, "Miles of trail");
            Assert.AreEqual(1, model.Park.NumberOfCampsites, "Number of campsites");
            Assert.AreEqual(2018, model.Park.YearFounded, "Year founded");
            Assert.AreEqual(1, model.Park.AnnualVisitorCount, "Annual visitor count");
            Assert.AreEqual(1, model.Park.EntryFee, "Entry fee");
            Assert.AreEqual(1, model.Park.NumberOfAnimalSpecies, "Number of animal species");
        }
    }
}
