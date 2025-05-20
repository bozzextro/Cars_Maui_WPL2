using CoolCars.Business.Entities;
using CoolCars.MAUI.Services;
using System.Collections.ObjectModel;
using Microsoft.Maui.Graphics;

namespace CoolCars.MAUI;

public partial class MainPage : ContentPage
{
	private readonly RestService _restService;
	private readonly SoundPlayerService _soundPlayerService;
	private readonly VideoPlayerService _videoPlayerService;
	private ObservableCollection<Car> _cars;
	private bool _isNightMode = false;

	public MainPage()
	{
		InitializeComponent();
		_restService = new RestService();
		_soundPlayerService = new SoundPlayerService();
		_videoPlayerService = new VideoPlayerService();
		_cars = new ObservableCollection<Car>();
		BindingContext = _cars;
		ApplyTheme();
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
			

			UpdateListViewCells(_isNightMode);
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
	
	private async void OnPlayVideoClicked(object sender, EventArgs e)
	{
		try
		{
			if (sender is Button button && button.CommandParameter is string carName)
			{
				string videoFileName = _videoPlayerService.GetVideoPathForCar(carName);
				await Navigation.PushAsync(new SimpleVideoPlayerPage(videoFileName));
			}
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Failed to play video: {ex.Message}", "OK");
		}
	}
	
	private void OnNightModeClicked(object sender, EventArgs e)
	{
		_isNightMode = !_isNightMode;
		ApplyTheme();
	}
	
	private void UpdateListViewCells(bool isDarkMode)
	{

		var itemsSource = CarsListView.ItemsSource;
		CarsListView.ItemsSource = null;
		CarsListView.ItemsSource = itemsSource;
	}
	
	private void UpdateStyles(bool isDarkMode)
	{
		var resources = Resources;
		
		if (isDarkMode)
		{

			((Style)resources["CardStyle"]).Setters.Clear();
			((Style)resources["CardStyle"]).Setters.Add(new Setter { Property = Grid.BackgroundColorProperty, Value = Color.FromArgb("#1E1E1E") });
			
			((Style)resources["HeaderTextStyle"]).Setters.Clear();
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#BB86FC") });
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = 22 });
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
			
			((Style)resources["InfoPanelStyle"]).Setters.Clear();
			((Style)resources["InfoPanelStyle"]).Setters.Add(new Setter { Property = StackLayout.BackgroundColorProperty, Value = Color.FromArgb("#2D2D2D") });
			
			((Style)resources["SectionHeaderStyle"]).Setters.Clear();
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#BB86FC") });
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = 16 });
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
			
			((Style)resources["InfoTextStyle"]).Setters.Clear();
			((Style)resources["InfoTextStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#E0E0E0") });
		}
		else
		{

			((Style)resources["CardStyle"]).Setters.Clear();
			((Style)resources["CardStyle"]).Setters.Add(new Setter { Property = Grid.BackgroundColorProperty, Value = Colors.White });
			
			((Style)resources["HeaderTextStyle"]).Setters.Clear();
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#1A237E") });
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = 22 });
			((Style)resources["HeaderTextStyle"]).Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
			
			((Style)resources["InfoPanelStyle"]).Setters.Clear();
			((Style)resources["InfoPanelStyle"]).Setters.Add(new Setter { Property = StackLayout.BackgroundColorProperty, Value = Color.FromArgb("#F5F5F5") });
			
			((Style)resources["SectionHeaderStyle"]).Setters.Clear();
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#424242") });
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.FontSizeProperty, Value = 16 });
			((Style)resources["SectionHeaderStyle"]).Setters.Add(new Setter { Property = Label.FontAttributesProperty, Value = FontAttributes.Bold });
			
			((Style)resources["InfoTextStyle"]).Setters.Clear();
			((Style)resources["InfoTextStyle"]).Setters.Add(new Setter { Property = Label.TextColorProperty, Value = Color.FromArgb("#616161") });
		}
	}
	
	private void ApplyTheme()
	{
		if (_isNightMode)
		{

			BackgroundColor = Color.FromArgb("#121212");
			NightModeButton.Text = "☀️";
			NightModeButton.BackgroundColor = Color.FromArgb("#BB86FC");
			

			CarsListView.BackgroundColor = Color.FromArgb("#121212");
			CarsListView.SeparatorColor = Color.FromArgb("#333333");
			

			UpdateStyles(true);
			

			UpdateListViewCells(true);
		}
		else
		{

			BackgroundColor = Color.FromArgb("#f0f0f0");
			NightModeButton.Text = "🌙";
			NightModeButton.BackgroundColor = Color.FromArgb("#673AB7");
			

			CarsListView.BackgroundColor = Colors.Transparent;
			CarsListView.SeparatorColor = Color.FromArgb("#e0e0e0");
			

			UpdateStyles(false);
			

			UpdateListViewCells(false);
		}
	}
}
