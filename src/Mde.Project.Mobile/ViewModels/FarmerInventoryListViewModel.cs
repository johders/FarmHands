﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
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
		private readonly OfferService _offerTestService;

		public FarmerInventoryListViewModel(IOfferService offerService, OfferService testSerevice)
		{
			_offerService = offerService;
			_offerTestService = testSerevice;

		}

		private ObservableCollection<Offer> offers;
		public ObservableCollection<Offer> Offers
		{
			get { return offers; }
			set
			{
				SetProperty(ref offers, value);
			}
		}

		public ICommand RefreshOffersListCommand => new Command(async () =>
		{
			//var result = await _offerService.GetAllAsync();
            var result = await _offerTestService.GetAllAsync();
			var offers = result.Data;
			Offers = new ObservableCollection<Offer>(offers);
		});

		public ICommand AddOfferCommand => new Command(async () =>
		{
			var navigationParameter = new Dictionary<string, object>
			{
				{nameof(FarmerInventoryEditViewModel.SelectedOffer), null}
			};

			await Shell.Current.GoToAsync(nameof(FarmerInventoryEditPage), true, navigationParameter);
		});

		public ICommand EditOfferCommand => new Command<Offer>(async (offer) =>
		{
			var navigationParameter = new Dictionary<string, object>
			{
				{nameof(FarmerInventoryEditViewModel.SelectedOffer), offer} 
			};

			await Shell.Current.GoToAsync(nameof(FarmerInventoryEditPage), true, navigationParameter);
		});

		public ICommand DeleteOfferCommand => new Command<Offer>(async (offer) =>
		{
			bool isConfirmed = await ShowDeleteConfirmationAsync(offer.Product.Name);
			BaseResultModel result = new();

			if (isConfirmed)
				//result = await _offerService.DeleteAsync(offer.Id);
				result = await _offerTestService.DeleteAsync(offer.Id);

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
