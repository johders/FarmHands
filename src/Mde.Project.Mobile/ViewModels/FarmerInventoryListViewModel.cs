using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models;
using Mde.Project.Mobile.Helpers;
using Mde.Project.Mobile.Pages.Farmer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmerInventoryListViewModel : ObservableObject
	{
		private readonly IOfferService _offerService;
		private readonly IFarmerService _farmerService;
		private readonly IImageConversionService _imageConversionService;

        public FarmerInventoryListViewModel(IOfferService offerService, IFarmerService farmerService, IImageConversionService imageConversionService)
        {
            _offerService = offerService;
            _farmerService = farmerService;
            _imageConversionService = imageConversionService;
        }

        private ObservableCollection<OfferViewModel> offers;
		public ObservableCollection<OfferViewModel> Offers
		{
			get { return offers; }
			set
			{
				SetProperty(ref offers, value);
			}
		}

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        public ICommand RefreshOffersListCommand => new Command(async () =>
		{
            IsLoading = true;

            var uid = await SecureStorage.GetAsync("userId");
            var farmerResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

            if (!farmerResult.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Oops", $"Unable to load offers at this time, try again later: {string.Join(", ", farmerResult.Errors)}", "OK");
                return;
            }

            var result = await _offerService.GetAllOffersByFarmIdAsync(farmerResult.Data);
			var offers = result.Data.Select(o => new OfferViewModel(o, _imageConversionService));
			Offers = new ObservableCollection<OfferViewModel>(offers);

            IsLoading = false;
        });

		public ICommand AddOfferCommand => new Command(async () =>
		{
			var navigationParameter = new Dictionary<string, object>
			{
				{nameof(FarmerInventoryEditViewModel.SelectedOffer), null}
			};

			await Shell.Current.GoToAsync(nameof(FarmerInventoryEditPage), true, navigationParameter);
		});

		public ICommand EditOfferCommand => new Command<OfferViewModel>(async (offer) =>
		{
			var navigationParameter = new Dictionary<string, object>
			{
				{nameof(FarmerInventoryEditViewModel.SelectedOffer), offer} 
			};

			await Shell.Current.GoToAsync(nameof(FarmerInventoryEditPage), true, navigationParameter);
		});

		public ICommand DeleteOfferCommand => new Command<OfferViewModel>(async (offer) =>
		{
			bool isConfirmed = await ShowDeleteConfirmationAsync(offer.Product.Name);
			BaseResultModel result = new();

            var token = await SecureStorage.Default.GetAsync("authToken");
            var roleString = await SecureStorage.Default.GetAsync("userRole");

            if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(roleString) || !Enum.TryParse(roleString, out UserRole role))
            {
                await Shell.Current.DisplayAlert("Error", "User authentication information is missing or invalid.", "OK");
                return;
            }

            if (isConfirmed)
				result = await _offerService.DeleteAsync(offer.Id, role);

			if (result.IsSuccess)
			{
				await ToastHelper.ShowToastAsync($"Offer for {offer.Product.Name} deleted!");
				RefreshOffersListCommand?.Execute(null);
			}

		});

		public async Task<bool> ShowDeleteConfirmationAsync(string offerItem)
		{
			return await Application.Current.MainPage.DisplayAlert("Confirm Delete", $"Are you sure you want to delete your offer for {offerItem}?", "Yes", "No");
		}
		
	}
}
