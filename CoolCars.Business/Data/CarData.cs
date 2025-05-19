using CoolCars.Business.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace CoolCars.Business.Data
{
    public class CarData
    {
        public SelectResult Select()
        {
            try
            {
                
                SelectResult result = new SelectResult();
                
                try
                {
                    using (SqlConnection connection = new SqlConnection(LocalSettings.ConnectionString))
                    {
                        connection.Open();
                        
                        string query = "SELECT Id, Name, ImageUrl, EngineSpecs, Price, Acceleration_0_100, Acceleration_0_200, QuarterMileTime, TopSpeed FROM Cars";
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {
                            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
                            {
                                result.Data = new DataTable();
                                adapter.Fill(result.Data);
                                result.Success = true;
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Success = false;
                    result.Message = ex.Message;
                    result.Exception = ex;
                }
                
                return result;
            }
            catch (Exception ex)
            {
                var result = new SelectResult { Success = false, Message = ex.Message, Exception = ex };
                return result;
            }
        }

        public BaseResult Insert(Car car)
        {
            try
            {
                string query = @"INSERT INTO Cars (Name, ImageUrl, EngineSpecs, Price, Acceleration_0_100, Acceleration_0_200, QuarterMileTime, TopSpeed) 
                               VALUES (@Name, @ImageUrl, @EngineSpecs, @Price, @Acceleration_0_100, @Acceleration_0_200, @QuarterMileTime, @TopSpeed)";
                
                using (SqlConnection connection = new SqlConnection(LocalSettings.ConnectionString))
                {
                    connection.Open();
                    
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@Name", car.Name);
                        command.Parameters.AddWithValue("@ImageUrl", car.ImageUrl);
                        command.Parameters.AddWithValue("@EngineSpecs", car.EngineSpecs);
                        command.Parameters.AddWithValue("@Price", car.Price);
                        command.Parameters.AddWithValue("@Acceleration_0_100", car.Acceleration_0_100);
                        command.Parameters.AddWithValue("@Acceleration_0_200", car.Acceleration_0_200);
                        command.Parameters.AddWithValue("@QuarterMileTime", car.QuarterMileTime);
                        command.Parameters.AddWithValue("@TopSpeed", car.TopSpeed);
                        
                        var result = new BaseResult();
                        try
                        {
                            command.ExecuteNonQuery();
                            result.Success = true;
                        }
                        catch (Exception ex)
                        {
                            result.Success = false;
                            result.Message = ex.Message;
                            result.Exception = ex;
                        }
                        
                        return result;
                    }
                }
            }
            catch (Exception ex)
            {
                return new BaseResult { Success = false, Message = ex.Message, Exception = ex };
            }
        }
    }
}
