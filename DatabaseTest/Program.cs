using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Text.Json;

namespace DatabaseTest
{
    class Program
    {
        public class Car
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string ImageUrl { get; set; }
            public string EngineSpecs { get; set; }
            public string Price { get; set; }
            public string Acceleration_0_100 { get; set; }
            public string Acceleration_0_200 { get; set; }
            public string QuarterMileTime { get; set; }
            public string TopSpeed { get; set; }
        }

        static void Main(string[] args)
        {
            string connectionString = @"Data Source=localhost\SQLEXPRESS;Initial Catalog=Cars;Integrated Security=True";
            Console.WriteLine($"Testing connection to: {connectionString}");
            
            try
            {
                List<Car> cars = new List<Car>();
                
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    Console.WriteLine("Connection opened successfully!");
                    
                    // Get all cars with all fields
                    string query = "SELECT Id, Name, ImageUrl, EngineSpecs, Price, Acceleration_0_100, Acceleration_0_200, QuarterMileTime, TopSpeed FROM Cars";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            Console.WriteLine("\nCars in database:");
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
                                Console.WriteLine($"Id: {car.Id}, Name: {car.Name}");
                            }
                        }
                    }
                    
                    connection.Close();
                    Console.WriteLine("Connection closed.");
                }
                
                // Convert to JSON
                string json = JsonSerializer.Serialize(cars, new JsonSerializerOptions { WriteIndented = true });
                Console.WriteLine("\nJSON representation:");
                Console.WriteLine(json);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner Exception: {ex.InnerException.Message}");
                }
            }
            
            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}
