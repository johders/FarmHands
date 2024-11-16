using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.User;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
	public class UserFavoriteFarmsViewModel : ObservableObject
	{
		private readonly IFavoriteFarmService _favoriteFarmService;
		private readonly IFarmService _farmService;


		public UserFavoriteFarmsViewModel(IFavoriteFarmService favoriteFarmService, IFarmService farmService)
		{
			_favoriteFarmService = favoriteFarmService;
			_farmService = farmService;
		}

		private ObservableCollection<FarmViewModel> favoriteFarms;
		public ObservableCollection<FarmViewModel> FavoriteFarms
		{
			get { return favoriteFarms; }
			set
			{
				SetProperty(ref favoriteFarms, value);
			}
		}

		public ICommand RefreshFavoriteFarmsListCommand => new Command(async () =>
		{
			var result = await _favoriteFarmService.GetAllFavoriteFarmsAsync();
			var favoriteFarms = result.Data.Select(ff => new FarmViewModel(ff, _farmService));

			FavoriteFarms = new ObservableCollection<FarmViewModel>(favoriteFarms);
		});

		public ICommand ViewFarmDetailsCommand => new Command<FarmViewModel>(async (farmViewModel) =>
		{
			var result = await _farmService.GetByIdAsync(farmViewModel.Id);
			var farm = result.Data;

			if (!result.IsSuccess)
			{
				Shell.Current.DisplayAlert("Oops", $"{String.Join(", ", result.Errors)}", "OK");
				return;
			}

			var navigationParameter = new Dictionary<string, object>()
			{
				{ nameof(UserFarmDetailsViewModel.SelectedFarm), farm }
			};

			await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
		});

	}
}
