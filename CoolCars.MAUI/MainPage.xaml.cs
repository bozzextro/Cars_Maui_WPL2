using CoolCars.Business.Entities;
using CoolCars.MAUI.Services;
using System.Collections.ObjectModel;

namespace CoolCars.MAUI;

public partial class MainPage : ContentPage
{
	private readonly RestService _restService;
	private ObservableCollection<Car> _cars;

	public MainPage()
	{
		InitializeComponent();
		_restService = new RestService();
		_cars = new ObservableCollection<Car>();
		BindingContext = _cars;
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
}
