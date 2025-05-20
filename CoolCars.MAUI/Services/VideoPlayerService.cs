using System;
using System.IO;
using System.Threading.Tasks;

namespace CoolCars.MAUI.Services
{
    public class VideoPlayerService
    {
        public string GetVideoPathForCar(string carName)
        {
            if (carName.Contains("Porsche", StringComparison.OrdinalIgnoreCase) || 
                carName.Contains("GT3", StringComparison.OrdinalIgnoreCase))
            {
                return "gt3rs.mp4";
            }
            else if (carName.Contains("BMW", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("M5", StringComparison.OrdinalIgnoreCase))
            {
                return "m5.mp4";
            }
            else if (carName.Contains("Mazda", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("RX7", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("RX-7", StringComparison.OrdinalIgnoreCase))
            {
                return "mazda.mp4";
            }
            else if (carName.Contains("Mercedes", StringComparison.OrdinalIgnoreCase))
            {
                return "mercedes.mp4";
            }
            
            return "m5.mp4";
        }
    }
}
