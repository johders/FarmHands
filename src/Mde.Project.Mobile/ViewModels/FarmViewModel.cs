using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;

namespace Mde.Project.Mobile.ViewModels
{
	public class FarmViewModel : ObservableObject
	{
		private readonly Farm _farm;
		private readonly IFarmService _farmService;
		private int? _offerCount;

		public FarmViewModel(Farm farm, IFarmService farmService)
		{
			_farm = farm;
			_farmService = farmService;
            if (farm.ImageUrl.Contains("data:image"))
            {
                ImageSource = ConvertToImageSource(farm.ImageUrl);
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

        private ImageSource ConvertToImageSource(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
                return null;

            try
            {
                var base64Data = base64String.Contains(",")
                    ? base64String.Substring(base64String.IndexOf(",") + 1)
                    : base64String;

                byte[] imageBytes = Convert.FromBase64String(base64Data);
                return ImageSource.FromStream(() => new MemoryStream(imageBytes));
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
