using CoolCars.Business.Entities;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace CoolCars.MAUI.Services
{
    public class RestService
    {
        private readonly HttpClient _client;

        private const string REST_URL = "https://t57ss5pg-7051.euw.devtunnels.ms/api/car";

        public RestService()
        {
            HttpClientHandler handler = new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true
            };

            _client = new HttpClient(handler);
        }

        public async Task<(List<Car> Cars, string ErrorMessage)> GetCarsAsync()
        {
            List<Car> cars = new List<Car>();
            string errorMessage = null;

            try
            {
                HttpResponseMessage response = await _client.GetAsync(REST_URL);
                
                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    
                    if (!string.IsNullOrEmpty(content))
                    {
                        try
                        {
                            cars = JsonConvert.DeserializeObject<List<Car>>(content);
                            if (cars == null || cars.Count == 0)
                            {
                                errorMessage = "No cars found in the response data";
                            }
                        }
                        catch (JsonException jsonEx)
                        {
                            errorMessage = $"JSON parsing error: {jsonEx.Message}\nActual content: {content}";
                        }
                    }
                    else
                    {
                        errorMessage = "Empty response from server";
                    }
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    errorMessage = $"API Error: {response.StatusCode} - {errorContent}";
                }
            }
            catch (System.Exception ex)
            {
                errorMessage = $"Exception: {ex.Message}";
            }
            
            return (cars, errorMessage);
        }

        public async Task<(bool Success, string ErrorMessage)> AddCarAsync(Car car)
        {
            try
            {
                string json = JsonConvert.SerializeObject(car);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _client.PostAsync(REST_URL, content);
                
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (System.Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteCarAsync(int carId)
        {
            try
            {
                HttpResponseMessage response = await _client.DeleteAsync($"{REST_URL}/{carId}");
                
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (System.Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }

        public async Task<(bool Success, string ErrorMessage)> DeleteMultipleCarsAsync(List<int> carIds)
        {
            try
            {
                string json = JsonConvert.SerializeObject(carIds);
                StringContent content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                
                HttpResponseMessage response = await _client.DeleteAsync($"{REST_URL}/multiple?ids={string.Join(",", carIds)}");
                
                if (response.IsSuccessStatusCode)
                {
                    return (true, null);
                }
                else
                {
                    string errorContent = await response.Content.ReadAsStringAsync();
                    return (false, $"API Error: {response.StatusCode} - {errorContent}");
                }
            }
            catch (System.Exception ex)
            {
                return (false, $"Exception: {ex.Message}");
            }
        }
    }
}
