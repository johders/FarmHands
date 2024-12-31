using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	[QueryProperty(nameof(SelectedOffer), nameof(SelectedOffer))]
	public class UserOfferDetailsViewModel : ObservableObject
    {
		private readonly IFarmService _farmService;
        private readonly IImageConversionService _imageConversionService;

        public UserOfferDetailsViewModel(IFarmService farmService, IImageConversionService imageConversionService)
        {
            _farmService = farmService;
            _imageConversionService = imageConversionService;
        }


		private OfferViewModel selectedOffer;
		public OfferViewModel SelectedOffer
		{
			get { return selectedOffer; }
			set
			{
				if (SetProperty(ref selectedOffer, value))
				{
					OnPropertyChanged(nameof(FormattedPrice));
				}
			}
		}

		public string FormattedPrice => $"{SelectedOffer?.Price:C2}/{SelectedOffer?.Unit}";

		public ICommand ViewFarmDetailsCommand => new Command<Farm>(async (farm) =>
		{
			var farmViewModel = new FarmViewModel(farm, _farmService, _imageConversionService);
			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserFarmDetailsViewModel.SelectedFarm), farmViewModel }
			};

			await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
		});
	}
}
