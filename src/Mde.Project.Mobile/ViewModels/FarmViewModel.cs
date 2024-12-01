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
            //_ = LoadOfferCountAsync();

            _ = Task.Run(() => LoadOfferCountAsync());
        }

		public string Id => _farm.Id;
		public string Name => _farm.Name;
		public string ImageUrl => _farm.ImageUrl;
		public int? OfferCount
		{
			get => _offerCount;
			private set => SetProperty(ref _offerCount, value);
		}
		private async Task LoadOfferCountAsync()
		{
			OfferCount = await _farmService.GetOfferCountAsync(_farm.Id);
		}
	}
}
