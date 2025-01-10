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
        private readonly IImageConversionService _imageConversionService;
        private MemoryStream _imageStream;

        public ProductViewModel(Product product, IProductService productService, IImageConversionService imageConversionService)
        {
            _product = product;
            _productService = productService;
            _imageConversionService = imageConversionService;

            if (product.ImageUrl.Contains("data:image"))
            {
                using var imageStream = _imageConversionService.ConvertBase64ToStream(product.ImageUrl);

                if (imageStream != null)
                {
                    _imageStream = new MemoryStream();
                    imageStream.CopyTo(_imageStream);
                    _imageStream.Position = 0;

                    ImageSource = ImageSource.FromStream(() =>
                    {
                        var streamClone = new MemoryStream(_imageStream.ToArray());
                        return streamClone;
                    });
                }
            }
        }

        public string Id => _product.Id;
        public string Description => _product.Description;
        public ImageSource ImageSource { get; private set; }
        public string Name => _product.Name;
		public string ImageUrl => _product.ImageUrl;
		public int? OfferCount
		{
			get => _offerCount;
			private set => SetProperty(ref _offerCount, value);
		}
        public async Task LoadOfferCountAsync()
		{
			OfferCount = await _productService.GetOfferCountAsync(_product.Id);
		}
	}
}
