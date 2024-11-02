using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Mobile.Pages.User;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	[QueryProperty(nameof(SelectedOffer), nameof(SelectedOffer))]
	public class UserOfferDetailsViewModel : ObservableObject
    {

		private Offer selectedOffer;
		public Offer SelectedOffer
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
			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
			};

			await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
		});
	}
}
