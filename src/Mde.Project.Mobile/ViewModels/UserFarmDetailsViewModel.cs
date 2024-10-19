using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedFarm), nameof(SelectedFarm))]
    public class UserFarmDetailsViewModel : ObservableObject
    {
        private readonly IOfferService _offerService;

        public UserFarmDetailsViewModel(IOfferService offerService)
        {
            _offerService = offerService;
        }

        private Farm selectedFarm;

        public Farm SelectedFarm
        {
            get { return selectedFarm; }
            set 
            {
               if(SetProperty(ref selectedFarm, value))
                {
                    _ = LoadOffersForSelectedFarm();
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

        private async Task LoadOffersForSelectedFarm()
        {
            if (SelectedFarm is not null)
            {
                var result = await _offerService.GetAllOffersByFarmIdAsync(SelectedFarm.Id);
                var offers = result.Data;
                Offers = new ObservableCollection<Offer>(offers);
            }
        }
    }
}
