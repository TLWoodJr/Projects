using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;


namespace Capstone.Web.DAL
{
    public class SurveySqlDAL:ISurveyDAL
    {
        private string _connectionString;
        private const string _getLastIdSQL = "SELECT CAST(SCOPE_IDENTITY() as int);";

        public SurveySqlDAL (string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Insert survey details into database
        /// </summary>
        /// <param name="survey"></param>
        /// <returns>bool to indicate if adding details succeeded</returns>
        public bool AddSurvey(SurveyModel survey)
        {
            int surveyID = 0;
            bool success = false;
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                string SQLCommand = "INSERT INTO survey_result (parkCode, emailAddress, state, activityLevel) " +
                                    "VALUES ((SELECT parkCode from park where parkname = @parkName), @emailAddress, @state, @activityLevel);";
                SqlCommand cmd = new SqlCommand(SQLCommand+_getLastIdSQL);
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("@parkName", survey.ParkName);
                cmd.Parameters.AddWithValue("@emailAddress", survey.Email);
                cmd.Parameters.AddWithValue("@state", survey.ResidenceState);
                cmd.Parameters.AddWithValue("@activityLevel", survey.ActivityLevel);

                surveyID = (int)cmd.ExecuteScalar();
            }
            if (surveyID > 0)
            {
                success = true;
            }
            return success;
        }

        /// <summary>
        /// Returns list of favorite parks based on survey results gathered in the database
        /// </summary>
        /// <returns>list of FavoriteParkViewModel objects</returns>
        public List<FavoriteParkViewModel> GetFavoriteParks()
        {
            List<FavoriteParkViewModel> result = new List<FavoriteParkViewModel>();
            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT COUNT(*) as 'Votes', survey_result.parkCode, parkName FROM survey_result " +
                                                 "JOIN park on survey_result.parkCode = park.parkCode " +
                                                 "GROUP BY survey_result.parkCode, parkName " +
                                                 "ORDER BY 'Votes' DESC;");
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    FavoriteParkViewModel park = new FavoriteParkViewModel()
                    {
                        ParkCode = Convert.ToString(reader["parkCode"]),
                        Votes = Convert.ToInt32(reader["Votes"]),
                        ParkName = Convert.ToString(reader["parkName"])
                    };
                    result.Add(park);
                }
            }
            return result;
        }

        /// <summary>
        /// Gets a list of park names from the database
        /// </summary>
        /// <returns>List of strings of park names</returns>
        public List<string> GetParkNames()
        {
            List<string> parks = new List<string>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Select parkName from park";

                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    parks.Add((string)reader["parkName"]);
                }
            }
            return parks;
        }
    }
}