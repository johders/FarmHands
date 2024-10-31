﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedFarm), nameof(SelectedFarm))]
    public class UserFarmDetailsViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;
        private readonly IFavoriteFarmService _favoriteFarmService;

        public UserFarmDetailsViewModel(IOfferService offerService, IFavoriteFarmService favoriteFarmService)
        {
            _offerService = offerService;
            _favoriteFarmService = favoriteFarmService;
        }

        private Farm selectedFarm;
        public Farm SelectedFarm
        {
            get { return selectedFarm; }
            set 
            {
               if(SetProperty(ref selectedFarm, value))
                {
                    _ = LoadOffersForSelectedFarmAsync();
                }
            }
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

        private bool isFavorite;

        public bool IsFavorite
        {
            get { return isFavorite; }
            set 
            { 
                SetProperty(ref isFavorite, value);
            }
        }

		public ICommand CheckIfFarmIsFavoriteCommand => new Command(async () =>
		{
			if (SelectedFarm != null)
			{
				var result = await _favoriteFarmService.IsFavoritedAsync(SelectedFarm.Id);
				IsFavorite = result.IsSuccess;
			}
		});

		public ICommand ToggleFavoriteCommand => new Command(async () =>
		{
			if (IsFavorite)
			{
				await _favoriteFarmService.DeleteAsync(SelectedFarm.Id);
			}
			else
			{
				await _favoriteFarmService.CreateAsync(SelectedFarm.Id);
			}
			IsFavorite = !IsFavorite;
		});

		private async Task LoadOffersForSelectedFarmAsync()
		{
			if (SelectedFarm is not null)
			{
				var result = await _offerService.GetAllOffersByFarmIdAsync(SelectedFarm.Id);
				var offers = result.Data;
				Offers = new ObservableCollection<Offer>(offers);
			}
		}

		public ICommand ViewOfferDetailsCommand => new Command<Offer>(async (offer) =>
		{

			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserOfferDetailsViewModel.SelectedOffer), offer }
			};

			await Shell.Current.GoToAsync(nameof(UserOfferDetailPage), true, navigationParameter);
		});
	}
}
