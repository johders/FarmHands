using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;

namespace Mde.Project.Mobile.ViewModels
{
	public class OfferViewModel : ObservableObject
	{
		private readonly Offer _offer;
		private bool _isAvailable;

		public OfferViewModel(Offer offer)
		{
			_offer = offer;
			_isAvailable = offer.IsAvailable;
		}

		public string ProductName => _offer.Product.Name;
		public string VariantName => _offer.Variant;
		public string OfferImageUrl => _offer.OfferImageUrl;
		public decimal Price => _offer.Price;
		public Unit Unit => _offer.Unit;

		public bool IsAvailable
		{
			get => _offer.IsAvailable;
			set
			{
				if (SetProperty(ref _isAvailable, value))
				{
					_offer.IsAvailable = value;
					OnPropertyChanged(nameof(IsAvailable));
				}
			}
		}
	}
}
