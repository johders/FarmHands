using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Mobile.ViewModels
{
	public class ProductViewModel : ObservableObject
	{
		private readonly Product _product;
		private readonly IProductService _productService;
		private int? _offerCount;

		public ProductViewModel(Product product, IProductService productService)
		{
			_product = product;
			_productService = productService;
			_ = LoadOfferCountAsync();
		}

		public string Id => _product.Id;

		public string Name => _product.Name;
		public string ImageUrl => _product.ImageUrl;
		public int? OfferCount
		{
			get => _offerCount;
			private set => SetProperty(ref _offerCount, value);
		}
		private async Task LoadOfferCountAsync()
		{
			OfferCount = await _productService.GetOfferCountAsync(_product.Id);
		}
	}
}
