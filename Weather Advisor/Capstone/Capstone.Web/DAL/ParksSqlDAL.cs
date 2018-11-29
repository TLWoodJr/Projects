using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using Capstone.Web.Models;

namespace Capstone.Web.DAL
{
    public class ParksSqlDAL : IParksDAL
    {
        private string _connectionString;

        public ParksSqlDAL(string connectionString)
        {
            _connectionString = connectionString;
        }

        /// <summary>
        /// Calls database to return a list of Park View Models
        /// </summary>
        /// <returns></returns>
        public List<ParksHomeViewModel> GetParks()
        {
            List<ParksHomeViewModel> parks = new List<ParksHomeViewModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();

                cmd.CommandText = "Select parkCode, parkName, parkDescription from park";

                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    ParksHomeViewModel park = new ParksHomeViewModel()
                    {
                        ParkCode = (string)reader["parkCode"],
                        Name = (string)reader["parkName"],
                        Description = (string)reader["parkDescription"]
                    };

                    parks.Add(park);
                }
            }
            return parks;
        }

        /// <summary>
        /// Returns all information for a single park
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns>Park information for individual detail page</returns>
        public DetailViewModel GetDetails(string parkCode)
        {
            DetailViewModel viewModel = new DetailViewModel();
            List<WeatherDayModel> forecast = GetFiveDayForecast(parkCode);

            ParksModel park = new ParksModel()
            {
                ParkCode = parkCode
            };

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT weather.*, park.* FROM park " +
                                                 "JOIN weather on park.parkCode = weather.parkCode " +
                                                 "WHERE park.parkCode = @parkCode");

                cmd.Parameters.AddWithValue("@parkCode", parkCode);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    park.ParkName = Convert.ToString(reader["parkName"]);
                    park.ParkDescription = Convert.ToString(reader["parkDescription"]);
                    park.State = Convert.ToString(reader["state"]);
                    park.Acreage = Convert.ToInt32(reader["acreage"]);
                    park.MilesOfTrail = Convert.ToDouble(reader["milesOfTrail"]);
                    park.NumberOfCampsites = Convert.ToInt32(reader["numberOfCampsites"]);
                    park.Climate = Convert.ToString(reader["climate"]);
                    park.Elevation = Convert.ToInt32(reader["elevationInFeet"]);
                    park.YearFounded = Convert.ToInt32(reader["yearFounded"]);
                    park.AnnualVisitorCount = Convert.ToInt32(reader["annualVisitorCount"]);
                    park.InspirationalQuote = Convert.ToString(reader["inspirationalQuote"]);
                    park.InspirationalQuoteSource = Convert.ToString(reader["inspirationalQuoteSource"]);
                    park.EntryFee = Convert.ToInt32(reader["entryFee"]);
                    park.NumberOfAnimalSpecies = Convert.ToInt32(reader["numberOfAnimalSpecies"]);

                }
            }
            viewModel.Park = park;
            viewModel.FiveDayForecast = forecast;

            return viewModel;
        }

        /// <summary>
        /// Returns list of weather forecasts for use in GetDetails method
        /// </summary>
        /// <param name="parkCode"></param>
        /// <returns>list of weather forecasts</returns>
        public List<WeatherDayModel> GetFiveDayForecast(string parkCode)
        {
            List<WeatherDayModel> forecast = new List<WeatherDayModel>();

            using (SqlConnection conn = new SqlConnection(_connectionString))
            {
                conn.Open();

                SqlCommand cmd = new SqlCommand(@"SELECT weather.* FROM weather " +
                                                 "WHERE parkCode = @parkCode");

                cmd.Parameters.AddWithValue("@parkCode", parkCode);
                cmd.Connection = conn;

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    WeatherDayModel day = new WeatherDayModel();
                    day.FiveDayForecastValue = (int)reader["fiveDayForecastValue"];
                    string forecastfield = (string)reader["forecast"];
                    if(forecastfield == "partly cloudy")
                    {
                        forecastfield = "partlyCloudy";
                    }
                    day.Forecast = forecastfield;
                    day.High = Convert.ToDouble(reader["high"]);
                    day.Low = Convert.ToDouble(reader["low"]);
                    day.ParkCode = parkCode;
                    forecast.Add(day);
                }

                return forecast;
            }
        }
    }
}