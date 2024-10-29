using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmerSettingsViewModel : ObservableObject
	{
		private readonly IFarmService _farmService;
		private Farm _farm;

		public FarmerSettingsViewModel(IFarmService farmService)
		{
			_farmService = farmService;
			Initialize();
		}

		private async void Initialize()
		{
			await GetMockFarm();
		}

		private async Task GetMockFarm()
		{
			var result = await _farmService.GetByIdAsync(Guid.Parse("10000000-0000-0000-0000-000000000007"));
			
			if (result.IsSuccess)
			{
				_farm = result.Data.First();
			}		
		}

		private string _name;
		public string Name
		{
			get => _name;
			set => SetProperty(ref _name, value);
		}

		private string _description;
		public string Description
		{
			get => _description;
			set => SetProperty(ref _description, value);
		}

		private double _latitude;
		public double Latitude
		{
			get => _latitude;
			set => SetProperty(ref _latitude, value);
		}

		private double _longitude;
		public double Longitude
		{
			get => _longitude;
			set => SetProperty(ref _longitude, value);
		}

		private string _imageUrl;
		public string ImageUrl
		{
			get => _imageUrl;
			set => SetProperty(ref _imageUrl, value);
		}

		public ICommand SaveCommand => new Command(async () =>
		{
			var updateModel = new FarmUpdateRequestModel
			{
				Id = _farm.Id,
				Name = Name,
				Description = Description,
				Latitude = Latitude,
				Longitude = Longitude,
				ImageUrl = ImageUrl
			};

			var result = await _farmService.UpdateAsync(updateModel);

			if (result.IsSuccess)
			{
				await ToastHelper.ShowToastAsync($"Changes successfully saved");
			}
			else
			{
				await Application.Current.MainPage.DisplayAlert("Error", "Failed to update farm details.", "OK");
			}
		});
	}
}
