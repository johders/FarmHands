using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmerSettingsViewModel : ObservableObject
	{
		private readonly IFarmService _farmService;
		private Farm farm;
        public FarmerSettingsViewModel(IFarmService farmService)
        {
            _farmService = farmService;

            Initialize();

            if (farm is not null)
            {
                Name = farm.Name;
                Description = farm.Description;
                Latitude = farm.Latitude;
                Longitude = farm.Longitude;
                ImageUrl = farm.ImageUrl;
            }
            
        }

        private async void Initialize()
		{
			await GetMockFarm();
		}

		private async Task GetMockFarm()
		{
			var result = await _farmService.GetByIdAsync("10000000-0000-0000-0000-000000000007");
			
			if (result.IsSuccess)
			{
				farm = result.Data;
			}		
		}


		private string name;
		public string Name
		{
			get => name;
			set => SetProperty(ref name, value);
		}

		private string description;
		public string Description
		{
			get => description;
			set => SetProperty(ref description, value);
		}

		private double latitude;
		public double Latitude
		{
			get => latitude;
			set => SetProperty(ref latitude, value);
		}

		private double longitude;
		public double Longitude
		{
			get => longitude;
			set => SetProperty(ref longitude, value);
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
				Id = farm.Id,
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

		public ICommand SwitchToUserViewCommand => new Command(() =>
		{
			App.Current.MainPage = new AppShellUser();
		});

		public ICommand LogOutCommand => new Command(async () =>
		{
			bool isConfirmed = await ShowLogoutConfirmationAsync();

			if (isConfirmed)
			{
				Application.Current.MainPage = new AppShellStartup();
			}
		});

		public async Task<bool> ShowLogoutConfirmationAsync()
		{
			return await Application.Current.MainPage.DisplayAlert("Confirm signout", "Are you sure you want to sign out?", "Yes", "No");
		}
	}
}
