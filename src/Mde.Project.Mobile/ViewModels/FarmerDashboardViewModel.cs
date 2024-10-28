﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmerDashboardViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;

		public FarmerDashboardViewModel(IOfferService offerService)
		{
			_offerService = offerService;
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

		public ICommand RefreshOffersListCommand =>
			new Command(async () =>
			{
				var result = await _offerService.GetAllAsync();
				var offers = result.Data;
				Offers = new ObservableCollection<Offer>(offers);
			});

		public ICommand GoToInventoryManagerCommand => new Command(async () =>
		{
			await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);
		});
	}
}
