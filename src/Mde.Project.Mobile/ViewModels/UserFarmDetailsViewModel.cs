using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using System.Collections.ObjectModel;

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
                    _ = CheckIfFarmIsFavoritedAsync();
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

        private async Task LoadOffersForSelectedFarmAsync()
        {
            if (SelectedFarm is not null)
            {
                var result = await _offerService.GetAllOffersByFarmIdAsync(SelectedFarm.Id);
                var offers = result.Data;
                Offers = new ObservableCollection<Offer>(offers);
            }
        }

        private async Task CheckIfFarmIsFavoritedAsync()
        {
            if (SelectedFarm is not null)
            {
                try
                {
                    var result = await _favoriteFarmService.IsFavoritedAsync(SelectedFarm.Id);
                    IsFavorite = result.IsSuccess;
                }
                catch(Exception ex)
                {
                    var result = ex.Message;
                }
            }
        }
    }
}
