using System;
using System.IO;
using System.Threading.Tasks;

namespace CoolCars.MAUI
{
    public partial class SimpleVideoPlayerPage : ContentPage
    {
        public SimpleVideoPlayerPage(string videoFileName)
        {
            InitializeComponent();
            LoadVideo(videoFileName);
        }

        private async void LoadVideo(string videoFileName)
        {
            try
            {
                string htmlContent = $@"
                    <html>
                    <head>
                        <style>
                            body {{ margin: 0; padding: 0; background-color: black; }}
                            video {{ width: 100%; height: 100%; }}
                        </style>
                    </head>
                    <body>
                        <video controls autoplay>
                            <source src=""file:///android_asset/Videos/{videoFileName}"" type=""video/mp4"">
                        </video>
                    </body>
                    </html>";

                videoWebView.Source = new HtmlWebViewSource { Html = htmlContent };
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load video: {ex.Message}", "OK");
            }
        }

        private async void OnCloseClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}
