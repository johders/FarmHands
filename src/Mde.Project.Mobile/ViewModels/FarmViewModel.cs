using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmViewModel : ObservableObject
	{
		private readonly Farm _farm;
		private readonly IFarmService _farmService;
        private readonly IImageConversionService _imageConversionService;
        private MemoryStream _imageStream;
        private int? _offerCount;

        public FarmViewModel(Farm farm, IFarmService farmService, IImageConversionService imageConversionService)
        {
            _farm = farm;
            _farmService = farmService;
            _imageConversionService = imageConversionService;

            if (farm.ImageUrl.Contains("data:image"))
            {
                using var imageStream = _imageConversionService.ConvertBase64ToStream(farm.ImageUrl);

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
            _ = Task.Run(() => LoadOfferCountAsync());
        }

        public string Id => _farm.Id;
		public string Name => _farm.Name;
		public string ImageUrl => _farm.ImageUrl;
		public string Description => _farm.Description;
        public ImageSource ImageSource {  get; private set; }
        public int? OfferCount
		{
			get => _offerCount;
			private set => SetProperty(ref _offerCount, value);
		}
		private async Task LoadOfferCountAsync()
		{
			OfferCount = await _farmService.GetOfferCountAsync(_farm.Id);
		}

        public void Dispose()
        {
            _imageStream?.Dispose();
        }
    }
}
