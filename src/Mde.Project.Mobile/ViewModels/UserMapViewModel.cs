using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Syncfusion.Maui.Maps;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class UserMapViewModel : ObservableObject
    {
        private readonly IFarmService _farmService;
        private readonly IGeolocation _geolocation;
        public ObservableCollection<Farm> Farms { get; set; } = new();

        public UserMapViewModel(IFarmService farmService, IGeolocation geolocation)
        {
            _farmService = farmService;
            _geolocation = geolocation;
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
                var location = await _geolocation.GetLastKnownLocationAsync();

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

                    //Markers.Add(new MapMarker
                    //{
                    //    Latitude = location.Latitude,
                    //    Longitude = location.Longitude
                    //});
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

                    Markers.Add(new MapMarker
                    {
                        Latitude = farm.Latitude,
                        Longitude = farm.Longitude,

                        //Content = new Label { Text = farm.Name, FontSize = 10, TextColor = Colors.Black }
                    });
                }
            }
        });

    }
}
