using CoolCars.Business.Entities;
using CoolCars.MAUI.Services;
using System.Collections.ObjectModel;

namespace CoolCars.MAUI;

public partial class MainPage : ContentPage
{
	private readonly RestService _restService;
	private readonly SoundPlayerService _soundPlayerService;
	private ObservableCollection<Car> _cars;

	public MainPage()
	{
		InitializeComponent();
		_restService = new RestService();
		_soundPlayerService = new SoundPlayerService();
		_cars = new ObservableCollection<Car>();
		BindingContext = _cars;
	}
	
	private async void OnAddCarClicked(object sender, EventArgs e)
	{
		await Navigation.PushAsync(new AddCarPage());
	}

	private async void OnLoadCarsClicked(object sender, EventArgs e)
	{
		try
		{
			var result = await _restService.GetCarsAsync();
			
			_cars.Clear();
			
			if (result.Cars != null)
			{
				foreach (var car in result.Cars)
				{
					_cars.Add(car);
				}
			}
			
			if (!string.IsNullOrEmpty(result.ErrorMessage))
			{
				await DisplayAlert("API Error", result.ErrorMessage, "OK");
			}
			else if (_cars.Count == 0)
			{
				await DisplayAlert("Info", "No cars found. Please make sure the Web API is running.", "OK");
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to load cars: {ex.Message}", "OK");
		}
	}
	
	private async void OnPlaySoundClicked(object sender, EventArgs e)
	{
		try
		{
			if (sender is Button button && button.CommandParameter is string carName)
			{
				await DisplayAlert("Info", $"Attempting to play sound for: {carName}", "OK");
				var result = await _soundPlayerService.PlayCarSoundAsync(carName);
				if (!result)
				{
					await DisplayAlert("Warning", "Sound could not be played. See debug output for details.", "OK");
				}
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to play sound: {ex.Message}", "OK");
		}
	}
}
