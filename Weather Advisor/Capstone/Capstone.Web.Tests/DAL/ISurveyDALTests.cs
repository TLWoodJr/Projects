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
    public class ISurveyDALTests
    {
        private TransactionScope _tran;
        private string connectionString = @"Data Source =.\SQLEXPRESS;Initial Catalog = NPGeek; Integrated Security = True";
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";
        private int _surveyCount = 0;
        private int _surveyID = 0;

        [TestInitialize]
        public void Initialize()
        {
            _tran = new TransactionScope();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd;
                connection.Open();

                //queries to test here
                cmd = new SqlCommand("INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                                    "VALUES ('CVNP', 'test@test.com', 'Ohio', 'sedentary');" + _getLastIdSQL, connection);
                _surveyID = (int)cmd.ExecuteScalar();

                cmd = new SqlCommand("SELECT COUNT(*) FROM survey_result;", connection);
                _surveyCount = (int)cmd.ExecuteScalar();
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            _tran.Dispose();
        }

        [TestMethod]
        public void GetFavoriteParksTest()
        {
            SurveySqlDAL surveySqlDAL = new SurveySqlDAL(connectionString);
            List<FavoriteParkViewModel> surveys = surveySqlDAL.GetFavoriteParks();

            Assert.IsNotNull(surveys);
            int totalVotes = 0;
            foreach(var model in surveys)
            {
                totalVotes += model.Votes;
            }
            Assert.AreEqual(_surveyCount, totalVotes);
        }
    }
}
