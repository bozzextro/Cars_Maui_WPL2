using CoolCars.Business.Data;
using CoolCars.Business.Entities;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CoolCars.Business
{
    public static class Cars
    {
        public static List<Car> GetCars()
        {
            List<Car> cars = new List<Car>();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(LocalSettings.ConnectionString))
                {
                    connection.Open();
                    
                    string query = "SELECT Id, Name, ImageUrl, EngineSpecs, Price, Acceleration_0_100, Acceleration_0_200, QuarterMileTime, TopSpeed FROM Cars";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                Car car = new Car
                                {
                                    Id = reader.GetInt32(0),
                                    Name = reader.GetString(1),
                                    ImageUrl = reader.GetString(2),
                                    EngineSpecs = reader.GetString(3),
                                    Price = reader.GetString(4),
                                    Acceleration_0_100 = reader.GetString(5),
                                    Acceleration_0_200 = reader.GetString(6),
                                    QuarterMileTime = reader.GetString(7),
                                    TopSpeed = reader.GetString(8)
                                };
                                
                                cars.Add(car);
                            }
                        }
                    }
                }
            }
            catch (System.Exception)
            {
                // Error handling if needed
            }
            
            return cars;
        }
        
        public static BaseResult Add(Car car)
        {
            CarData carData = new CarData();
            var result = carData.Insert(car);
            return result;
        }
        
        public static BaseResult Delete(int carId)
        {
            BaseResult result = new BaseResult();
            
            try
            {
                using (SqlConnection connection = new SqlConnection(LocalSettings.ConnectionString))
                {
                    connection.Open();
                    
                    string query = "DELETE FROM Cars WHERE Id = @Id";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.Add("@Id", SqlDbType.Int).Value = carId;
                        
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "Car not found";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            
            return result;
        }
        
        public static BaseResult DeleteMultiple(List<int> carIds)
        {
            BaseResult result = new BaseResult();
            
            if (carIds == null || carIds.Count == 0)
            {
                result.Success = false;
                result.Message = "No car IDs provided";
                return result;
            }
            
            try
            {
                using (SqlConnection connection = new SqlConnection(LocalSettings.ConnectionString))
                {
                    connection.Open();
                    

                    string idList = string.Join(",", carIds);
                    string query = $"DELETE FROM Cars WHERE Id IN ({idList})";
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            result.Success = true;
                        }
                        else
                        {
                            result.Success = false;
                            result.Message = "No cars found with the provided IDs";
                        }
                    }
                }
            }
            catch (System.Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            
            return result;
        }
    }
}
