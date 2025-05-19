using CoolCars.Business.Entities;
using CoolCars.MAUI.Services;

namespace CoolCars.MAUI
{
    public partial class AddCarPage : ContentPage
    {
        private readonly RestService _restService;
        
        public AddCarPage()
        {
            InitializeComponent();
            _restService = new RestService();
        }
        
        private async void OnAddCarClicked(object sender, EventArgs e)
        {
            if (ValidateInputs())
            {
                Car newCar = new Car
                {
                    Name = NameEntry.Text,
                    ImageUrl = ImageUrlEntry.Text,
                    EngineSpecs = EngineSpecsEntry.Text,
                    Price = PriceEntry.Text,
                    Acceleration_0_100 = Acceleration0100Entry.Text,
                    Acceleration_0_200 = Acceleration0200Entry.Text,
                    QuarterMileTime = QuarterMileTimeEntry.Text,
                    TopSpeed = TopSpeedEntry.Text
                };
                
                var result = await _restService.AddCarAsync(newCar);
                
                if (result.Success)
                {
                    await DisplayAlert("Success", "Car added successfully", "OK");
                    await Navigation.PopAsync();
                    return;
                }
                else
                {
                    await DisplayAlert("Error", $"Failed to add car: {result.ErrorMessage}", "OK");
                }
            }
        }
        
        private bool ValidateInputs()
        {
            if (string.IsNullOrWhiteSpace(NameEntry.Text))
            {
                DisplayAlert("Validation Error", "Car name is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(ImageUrlEntry.Text))
            {
                DisplayAlert("Validation Error", "Image URL is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(EngineSpecsEntry.Text))
            {
                DisplayAlert("Validation Error", "Engine specs are required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(PriceEntry.Text))
            {
                DisplayAlert("Validation Error", "Price is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Acceleration0100Entry.Text))
            {
                DisplayAlert("Validation Error", "0-100 km/h acceleration time is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(Acceleration0200Entry.Text))
            {
                DisplayAlert("Validation Error", "0-200 km/h acceleration time is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(QuarterMileTimeEntry.Text))
            {
                DisplayAlert("Validation Error", "Quarter mile time is required", "OK");
                return false;
            }
            
            if (string.IsNullOrWhiteSpace(TopSpeedEntry.Text))
            {
                DisplayAlert("Validation Error", "Top speed is required", "OK");
                return false;
            }
            
            return true;
        }
        
        private async void OnCancelClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
            return;
        }
    }
}
