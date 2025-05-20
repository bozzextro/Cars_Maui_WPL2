using System;
using System.IO;
using System.Threading.Tasks;
using Plugin.Maui.Audio;

namespace CoolCars.MAUI.Services
{
    public class SoundPlayerService
    {
        private readonly IAudioManager _audioManager;
        
        public SoundPlayerService()
        {
            _audioManager = AudioManager.Current;
        }
        
        public async Task<bool> PlayCarSoundAsync(string carName)
        {
            try
            {
                string soundFileName = GetSoundFileNameForCar(carName);
                if (string.IsNullOrEmpty(soundFileName))
                {
                    soundFileName = "v8_engine.mp3";
                }

                try
                {
                    using var stream = await FileSystem.OpenAppPackageFileAsync($"Sounds/{soundFileName}");
                    var player = _audioManager.CreatePlayer(stream);
                    player.Play();
                    return true;
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine($"Could not load sound file: {soundFileName}, Error: {ex.Message}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error playing sound: {ex.Message}");
                return false;
            }
        }

        private async Task<Stream> GetStreamFromFileAsync(string resourcePath)
        {
            try
            {
                try
                {
                    return await FileSystem.OpenAppPackageFileAsync($"Raw/{resourcePath}");
                }
                catch
                {
                    var assembly = GetType().Assembly;
                    var fullResourcePath = $"CoolCars.MAUI.Resources.Raw.{resourcePath}";
                    return assembly.GetManifestResourceStream(fullResourcePath);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to load resource: {ex.Message}");
                return null;
            }
        }

        private string GetSoundFileNameForCar(string carName)
        {
            if (carName.Contains("BMW", StringComparison.OrdinalIgnoreCase) || 
                carName.Contains("M5", StringComparison.OrdinalIgnoreCase))
            {
                return "bmw_m5_evosport_acc.mp3";
            }
            else if (carName.Contains("Porsche", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("GT3", StringComparison.OrdinalIgnoreCase))
            {
                return "porsche_gt3.mp3";
            }
            else if (carName.Contains("Mazda", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("RX7", StringComparison.OrdinalIgnoreCase) || 
                     carName.Contains("RX-7", StringComparison.OrdinalIgnoreCase))
            {
                return "rx7_best_sound.mp3";
            }
            
            return "v8_engine.mp3";
        }
    }
}
