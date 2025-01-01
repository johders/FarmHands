using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class FarmerSettingsViewModel : ObservableObject
    {
        private readonly IFarmService _farmService;
        private readonly IAccountService _accountService;
        private readonly IFarmerService _farmerService;
        private readonly IImageConversionService _imageConversionService;
        private readonly IOpenStreetService _openStreetService;
        private MemoryStream _imageStream;
        private Farm farm;
        public FarmerSettingsViewModel(IFarmService farmService, IAccountService accountService, IFarmerService farmerService,
            IImageConversionService imageConversionService, IOpenStreetService openStreetService)
        {
            _farmService = farmService;
            _accountService = accountService;
            _farmerService = farmerService;
            _imageConversionService = imageConversionService;
            _openStreetService = openStreetService;
        }

        private string addressInput;
        public string AddressInput
        {
            get => addressInput;
            set => SetProperty(ref addressInput, value);
        }

        private List<OpenStreetResult> searchResults;
        public List<OpenStreetResult> SearchResults
        {
            get => searchResults;
            set => SetProperty(ref searchResults, value);
        }

        private OpenStreetResult selectedAddress;
        public OpenStreetResult SelectedAddress
        {
            get => selectedAddress;
            set
            {
                if (SetProperty(ref selectedAddress, value))
                {
                    Latitude = selectedAddress?.Latitude ?? 0;
                    Longitude = selectedAddress?.Longitude ?? 0;
                }
            }
        }

        public ICommand SearchAddressCommand => new Command<string>(async (address) =>
        {
            if (string.IsNullOrWhiteSpace(address))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter a valid address.", "OK");
                return;
            }

            await GetCoordinatesFromAddressAsync(address);
        });

        public async Task GetCoordinatesFromAddressAsync(string address)
        {
            IsLoading = true;

            var result = await _openStreetService.GetCoordinatesFromAddressAsync(address);

            if (result.IsSuccess && result.Data != null)
            {
                SearchResults = result.Data;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Unable to find coordinates for the provided address.", "OK");
            }

            IsLoading = false;
        }

        public async Task InitializeAsync()
        {
            IsLoading = true;

            await GetUserFarm();

            if (farm is not null)
            {
                Name = farm.Name;
                Description = farm.Description;
                Latitude = farm.Latitude;
                Longitude = farm.Longitude;
                ImageUrl = farm.ImageUrl;
            }

            IsLoading = false;
        }

        private async Task GetUserFarm()
        {
            var uid = await SecureStorage.GetAsync("userId");
            var farmerResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

            var result = await _farmService.GetByIdAsync(farmerResult.Data);

            if (result.IsSuccess)
            {
                farm = result.Data;
            }
        }

        private bool isReplaceClicked;
        public bool IsReplaceClicked
        {
            get => isReplaceClicked;
            set => SetProperty(ref isReplaceClicked, value);
        }

        public ICommand ReplaceAddressCommand => new Command(() =>
        {
            IsReplaceClicked = true;
        });

        private string name;
        public string Name
        {
            get => name;
            set => SetProperty(ref name, value);
        }

        private bool isNameValid;
        public bool IsNameValid
        {
            get { return isNameValid; }
            set { SetProperty(ref isNameValid, value); }
        }

        private string description;
        public string Description
        {
            get => description;
            set => SetProperty(ref description, value);
        }

        private double latitude;
        public double Latitude
        {
            get => latitude;
            set => SetProperty(ref latitude, value);
        }

        private double longitude;
        public double Longitude
        {
            get => longitude;
            set => SetProperty(ref longitude, value);
        }

        private bool isLoading;
        public bool IsLoading
        {
            get { return isLoading; }
            set { SetProperty(ref isLoading, value); }
        }

        private string imageUrl;
        public string ImageUrl
        {
            get { return imageUrl; }
            set
            {
                if (SetProperty(ref imageUrl, value))
                {
                    if (!string.IsNullOrEmpty(value) && value.Contains("data:image"))
                    {
                        UpdateImageSource(value);
                    }
                    else
                    {
                        ImageSource = null;
                    }
                }
            }
        }

        private ImageSource _imageSource;
        public ImageSource ImageSource
        {
            get { return _imageSource; }
            private set { SetProperty(ref _imageSource, value); }
        }

        private void UpdateImageSource(string imageUrl)
        {
            using var imageStream = _imageConversionService.ConvertBase64ToStream(imageUrl);

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

        public ICommand ReplaceImageCommand => new Command(async () =>
        {
            var confirm = await Shell.Current.DisplayAlert("Replace Image", "Are you sure you want to replace the image?", "Yes", "No");
            if (confirm)
            {
                ImageUrl = null;
            }
        });

        public ICommand SelectImageCommand => new Command(async () =>
        {
            try
            {
                var result = await MediaPicker.PickPhotoAsync();

                if (result is not null)
                {
                    using var stream = await result.OpenReadAsync();
                    var base64Image = _imageConversionService.ConvertStreamToBase64(stream);
                    ImageUrl = $"data:image/jpeg;base64,{base64Image}";
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", "Unable to open file, try again later", "OK");
            }

        });

        public ICommand TakePictureCommand => new Command(async () =>
        {
            try
            {
                var result = await MediaPicker.CapturePhotoAsync();

                if (result is not null)
                {
                    using var stream = await result.OpenReadAsync();
                    var base64Image = _imageConversionService.ConvertStreamToBase64(stream);
                    ImageUrl = $"data:image/jpeg;base64,{base64Image}";
                }
            }
            catch (Exception ex)
            {
                var result = ex.Message;
                await Shell.Current.DisplayAlert("Error", $"Unable to take photo at this time, try again later: {ex.Message}", "OK");
            }
        });

        private bool IsProfileComplete(FarmUpdateRequestModel farmModel)
        {
            if (string.IsNullOrEmpty(Description) || string.IsNullOrEmpty(ImageUrl) || Latitude == 0 || Longitude == 0)
                return false;

            return true;
        }

        public ICommand SaveCommand => new Command(async () =>
        {

            if (!IsNameValid)
            {
                await Shell.Current.DisplayAlert("Validation Error", "Please enter your farm's name.", "OK");
                return;
            }

            var updateModel = new FarmUpdateRequestModel
            {
                Id = farm.Id,
                Name = Name,
                Description = Description,
                Latitude = Latitude,
                Longitude = Longitude,
                ImageUrl = ImageUrl
            };

            updateModel.ProfileComplete = IsProfileComplete(updateModel);

            if (!updateModel.ProfileComplete)
            {
                await Application.Current.MainPage
                .DisplayAlert("Profile incomplete", "Please note that your farm will only be visible when your profile is complete.", "OK");
            }

            var result = await _farmService.UpdateAsync(updateModel);

            if (result.IsSuccess)
            {
                await ToastHelper.ShowToastAsync($"Changes successfully saved");
                IsReplaceClicked = false;
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Failed to update farm details.", "OK");
            }
        });

        public ICommand SwitchToUserViewCommand => new Command(() =>
        {
            App.Current.MainPage = new AppShellUser();
        });

        public ICommand LogOutCommand => new Command(async () =>
        {
            bool isConfirmed = await ShowLogoutConfirmationAsync();

            if (isConfirmed)
            {
                var logoutResult = _accountService.Logout();

                if (!logoutResult.IsSuccess)
                {
                    await Application.Current.MainPage.DisplayAlert("Oops", "Signout issue. Please try again later", "OK");
                    return;
                }

                Application.Current.MainPage = new AppShellStartup();
            }
        });

        public async Task<bool> ShowLogoutConfirmationAsync()
        {
            return await Application.Current.MainPage.DisplayAlert("Confirm signout", "Are you sure you want to sign out?", "Yes", "No");
        }
    }
}
