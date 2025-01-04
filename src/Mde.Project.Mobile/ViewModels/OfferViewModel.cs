using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;

namespace Mde.Project.Mobile.ViewModels
{
    public class OfferViewModel : ObservableObject
    {
        private readonly Offer _offer;
        private bool _isAvailable;
        private readonly IImageConversionService _imageConversionService;
        private MemoryStream _imageStream;
        private readonly IOfferService _offerService;

        public OfferViewModel(Offer offer, IImageConversionService imageConversionService, IOfferService offerService)
        {
            _offer = offer;
            _isAvailable = offer.IsAvailable;
            _imageConversionService = imageConversionService;

            if (offer.OfferImageUrl.Contains("data:image"))
            {
                using var imageStream = _imageConversionService.ConvertBase64ToStream(offer.OfferImageUrl);

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

            _offerService = offerService;
        }
        public string Id => _offer.Id;
        public string ProductName => _offer.Product.Name;
        public string VariantName => _offer.Variant;
        public string FarmName => _offer.Farm.Name;
        public Farm Farm => _offer.Farm;
        public string OfferImageUrl => _offer.OfferImageUrl;
        public string Description => _offer.Description;
        public string ProductId => _offer.ProductId;
        public Product Product => _offer.Product;
        public string FarmId => _offer.FarmId;
        public ImageSource ImageSource { get; private set; }
        public decimal Price => _offer.Price;
        public Unit Unit => _offer.Unit;

        public bool IsAvailable
        {
            get => _offer.IsAvailable;
            set
            {
                if (SetProperty(ref _isAvailable, value))
                {
                    _offer.IsAvailable = value;
                    OnPropertyChanged(nameof(IsAvailable));
                    _ = UpdateAvailabilityAsync(value);
                }
            }
        }

        private async Task UpdateAvailabilityAsync(bool newValue)
        {
            var updateModel = new OfferEditRequestModel
            {
                Id = _offer.Id,
                IsAvailable = newValue,
                Farm = _offer.Farm,
                Product = _offer.Product
            };

            var result = await _offerService.UpdateAsync(updateModel, UserRole.Farmer);

            if (!result.IsSuccess)
            {
                await Shell.Current.DisplayAlert("Update Failed",
                    "Could not update the availability status. Please try again.", "OK");
                _isAvailable = !newValue;
                OnPropertyChanged(nameof(IsAvailable));
                return;
            }
            await ToastHelper.ShowToastAsync($"Offer availability for {updateModel.Product.Name} updated!");
        }
    }
}
