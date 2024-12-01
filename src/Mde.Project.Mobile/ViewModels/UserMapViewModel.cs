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
        public ObservableCollection<Farm> Farms { get; set; } = new();

        public UserMapViewModel(IFarmService farmService)
        {
            _farmService = farmService;
        }

        private MapMarkerCollection markers = new();
        public MapMarkerCollection Markers
        {
            get => markers;
            set => SetProperty(ref markers, value);
        }

        public ICommand RefreshFarmListCommand => new Command(async () =>
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
