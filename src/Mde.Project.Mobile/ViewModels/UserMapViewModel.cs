using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Helpers;
using Mde.Project.Mobile.Pages.User;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserMapViewModel : ObservableObject
    {
        private readonly IFarmService _farmService;
        private readonly IGeolocation _geolocation;
        private readonly IImageConversionService _imageConversionService;
        public ObservableCollection<Farm> Farms { get; set; } = new();

        public UserMapViewModel(IFarmService farmService, IGeolocation geolocation, IImageConversionService imageConversionService)
        {
            _farmService = farmService;
            _geolocation = geolocation;
            _imageConversionService = imageConversionService;
        }

        private MapMarkerCollection markers = new();
        public MapMarkerCollection Markers
        {
            get => markers;
            set => SetProperty(ref markers, value);
        }

        private MapLatLng centerLocation = new MapLatLng(0, 0);
        public MapLatLng CenterLocation
        {
            get => centerLocation;
            set => SetProperty(ref centerLocation, value);
        }

        public ICommand GetUserLocationCommand => new Command(async () =>
        {
            try
            {
                //var location = await _geolocation.GetLastKnownLocationAsync();
                var location = await _geolocation.GetLocationAsync();

                if (location == null)
                {
                    location = await _geolocation.GetLocationAsync(new GeolocationRequest
                    {
                        DesiredAccuracy = GeolocationAccuracy.Best,
                        Timeout = TimeSpan.FromSeconds(30)
                    });
                }

                if (location != null)
                {
                    CenterLocation = new MapLatLng(location.Latitude, location.Longitude);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Unable to get location: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        });

        public ICommand RefreshFarmMarkersCommand => new Command(async () =>
        {
            var result = await _farmService.GetAllAsync();

            if (result.IsSuccess && result.Data != null)
            {
                Farms.Clear();
                Markers.Clear();

                foreach (var farm in result.Data)
                {
                    Farms.Add(farm);

                    Markers.Add(new CustomMapMarker
                    {
                        Latitude = farm.Latitude,
                        Longitude = farm.Longitude,
                        Farm = farm
                    });
                }
            }
        });

        public ICommand ShowFarmDetailsCommand => new Command<Farm>(async (farm) =>
        {
            var farmViewModel = new FarmViewModel(farm, _farmService, _imageConversionService);

            var navigationParameter = new Dictionary<string, object>()
            {
                { nameof(UserFarmDetailsViewModel.SelectedFarm), farmViewModel }
            };

            await Shell.Current.GoToAsync(nameof(UserFarmDetailPage), true, navigationParameter);
        });

        public ICommand ToggleExpansionCommand => new Command<CustomMapMarker>((marker) =>
        {
            if (marker != null)
            {
                marker.IsExpanded = !marker.IsExpanded;
            }
        });

    }
}
