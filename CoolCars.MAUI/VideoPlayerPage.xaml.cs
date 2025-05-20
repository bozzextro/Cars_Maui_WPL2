using System;
using System.IO;
using System.Threading.Tasks;

namespace CoolCars.MAUI
{
    public partial class VideoPlayerPage : ContentPage
    {
        public VideoPlayerPage(string videoFileName)
        {
            InitializeComponent();
            LoadVideo(videoFileName);
        }

        private async void LoadVideo(string videoFileName)
        {
            try
            {
                string videoPath = await CopyVideoToCache(videoFileName);
                string htmlContent = GenerateVideoHtml(videoPath);
                videoWebView.Source = new HtmlWebViewSource { Html = htmlContent };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load video: {ex.Message}", "OK");
            }
        }

        private string GenerateVideoHtml(string videoPath)
        {
            return $@"<!DOCTYPE html>
<html>
<head>
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <style>
        body {{ margin: 0; padding: 0; background-color: #000; }}
        .video-container {{ 
            width: 100%; 
            height: 100vh; 
            display: flex; 
            align-items: center; 
            justify-content: center; 
        }}
        video {{ 
            max-width: 100%; 
            max-height: 100%; 
            width: auto; 
            height: auto; 
        }}
    </style>
</head>
<body>
    <div class=""video-container"">
        <video controls autoplay>
            <source src=""{videoPath}"" type=""video/mp4"">
            Your browser does not support the video tag.
        </video>
    </div>
</body>
</html>";
        }

        private async Task<string> CopyVideoToCache(string videoFileName)
        {
            try
            {
                string cacheDir = FileSystem.CacheDirectory;
                string targetPath = Path.Combine(cacheDir, videoFileName);

                if (!File.Exists(targetPath))
                {
                    using Stream sourceStream = await FileSystem.OpenAppPackageFileAsync($"Videos/{videoFileName}");
                    using FileStream targetStream = File.Create(targetPath);
                    await sourceStream.CopyToAsync(targetStream);
                }

                return targetPath;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error copying video: {ex.Message}");
                throw;
            }
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
