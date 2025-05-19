using Microsoft.Maui.Controls;
using System;
using System.Threading.Tasks;

namespace CoolCars.MAUI
{
    public partial class IntroPage : ContentPage
    {
        public IntroPage()
        {
            InitializeComponent();
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            
            var progressStep = 0.01;
            var delayMs = 30;
            
            for (double progress = 0; progress <= 1.0; progress += progressStep)
            {
                LoadingBar.Progress = progress;
                
                if (progress < 0.3)
                    LoadingLabel.Text = "Loading resources...";
                else if (progress < 0.6)
                    LoadingLabel.Text = "Connecting to server...";
                else if (progress < 0.9)
                    LoadingLabel.Text = "Almost there...";
                else
                    LoadingLabel.Text = "Ready!";
                
                await Task.Delay(delayMs);
            }
            
            await Navigation.PushAsync(new MainPage());
            Navigation.RemovePage(this);
        }
    }
}
