﻿using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedOffer), nameof(SelectedOffer))]
    public class FarmerInventoryEditViewModel : ObservableObject
    {
        private readonly IProductService _productService;
        private readonly IOfferService _offerService;
        private readonly IFarmService _farmService;
        private readonly IFarmerService _farmerService;
        private readonly IImageConversionService _imageConversionService;
        private MemoryStream _imageStream;
        private bool isAvailable;

        public FarmerInventoryEditViewModel(IProductService productService, IOfferService offerService, IFarmService farmService, IFarmerService farmerService, IImageConversionService imageConversionService)
        {
            _productService = productService;
            _offerService = offerService;
            _farmService = farmService;
            LoadUnitOptions();
            Products = new ObservableCollection<Product>();
            _farmerService = farmerService;
            _imageConversionService = imageConversionService;
        }

        public async Task InitializeAsync()
        {
            var result = await _productService.GetAllAsync();
            if (result.IsSuccess && result.Data != null)
            {
                foreach (var product in result.Data)
                {
                    Products.Add(product);
                }
            }
        }

        public ObservableCollection<Unit> UnitOptions { get; set; } = [];

        private string pageTitle;
        public string PageTitle
        {
            get { return pageTitle; }
            set { SetProperty(ref pageTitle, value); }
        }

        private OfferViewModel selectedOffer;
        public OfferViewModel SelectedOffer
        {
            get { return selectedOffer; }
            set
            {
                SetProperty(ref selectedOffer, value);

                if (selectedOffer is not null)
                {
                    PageTitle = "Edit offer";
                    SelectedProduct = selectedOffer.Product;
                    Variant = selectedOffer.VariantName;
                    Description = selectedOffer.Description;
                    Price = selectedOffer.Price;
                    SelectedUnit = selectedOffer.Unit;
                    ImageUrl = selectedOffer.OfferImageUrl;
                    isAvailable = selectedOffer.IsAvailable;
                    
                }
                else
                {
                    PageTitle = "Create new offer";
                    Variant = default;
                    SelectedProduct = default;
                    Description = default;
                    Price = default;
                    SelectedUnit = default;
                    ImageUrl = default;
                    isAvailable = true;
                }
            }
        }

        private ObservableCollection<Product> products;
        public ObservableCollection<Product> Products
        {
            get { return products; }
            set
            {
                SetProperty(ref products, value);
            }
        }

        private Product selectedProduct;
        public Product SelectedProduct
        {
            get { return selectedProduct; }
            set { SetProperty(ref selectedProduct, value); }
        }

        private string variant;
        public string Variant
        {
            get { return variant; }
            set { SetProperty(ref variant, value); }
        }

        private bool isVariantValid;
        public bool IsVariantValid
        {
            get { return isVariantValid; }
            set { SetProperty(ref isVariantValid, value); }
        }

        private string description;
        public string Description
        {
            get { return description; }
            set { SetProperty(ref description, value); }
        }

        private bool isDescriptionValid;
        public bool IsDescriptionValid
        {
            get { return isDescriptionValid; }
            set { SetProperty(ref isDescriptionValid, value); }
        }

        private Unit selectedUnit;
        public Unit SelectedUnit
        {
            get { return selectedUnit; }
            set { SetProperty(ref selectedUnit, value); }
        }

        private decimal price;
        public decimal Price
        {
            get { return price; }
            set
            {
                if (SetProperty(ref price, value))
                {
                    if (!isEditing)
                    {
                        PriceInput = price.ToString("N2", CultureInfo.InvariantCulture);
                    }
                }
            }
        }

        private string priceInput;
        public string PriceInput
        {
            get => priceInput;
            set
            {
                if (SetProperty(ref priceInput, value) && !isEditing)
                {
                    if (decimal.TryParse(priceInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var parsedValue))
                    {
                        Price = parsedValue;
                    }
                }
            }
        }

        private bool isEditing;
        public void StartEditing()
        {
            isEditing = true;
        }

        public void StopEditing()
        {
            isEditing = false;

            if (decimal.TryParse(PriceInput, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out var parsedValue))
            {
                Price = parsedValue;
            }
            else
            {
                PriceInput = Price.ToString("N2", CultureInfo.InvariantCulture);
            }
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

        private void LoadUnitOptions()
        {
            var units = Enum.GetValues(typeof(Unit)).Cast<Unit>();
            foreach (var unit in units)
            {
                UnitOptions.Add(unit);
            }
        }

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

        public ICommand ReplaceImageCommand => new Command(async () =>
        {
            var confirm = await Shell.Current.DisplayAlert("Replace Image", "Are you sure you want to replace the image?", "Yes", "No");
            if (confirm)
            {
                ImageUrl = null;
            }
        });

        public ICommand SaveCommand =>
            new Command(async () =>
            {

                if (!IsVariantValid)
                {
                    await Shell.Current.DisplayAlert("Validation Error", "Please enter the product variant.", "OK");
                    return;
                }

                if (!IsDescriptionValid)
                {
                    await Shell.Current.DisplayAlert("Validation Error", "Please enter the product description.", "OK");
                    return;
                }

                var productResult = await _productService.GetByIdAsync(SelectedProduct.Id);

                var uid = await SecureStorage.GetAsync("userId");
                var farmerResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

                if (!farmerResult.IsSuccess)
                {
                    await Shell.Current.DisplayAlert("Oops", $"Unable to create offer at this time, try again later: {string.Join(", ", farmerResult.Errors)}", "OK");
                    return;
                }

                var farmResult = await _farmService.GetByIdAsync(farmerResult.Data);

                var token = await SecureStorage.Default.GetAsync("authToken");
                var roleString = await SecureStorage.Default.GetAsync("userRole");

                if (string.IsNullOrEmpty(token) || string.IsNullOrEmpty(roleString) || !Enum.TryParse(roleString, out UserRole role))
                {
                    await Shell.Current.DisplayAlert("Error", "User authentication information is missing or invalid.", "OK");
                    return;
                }

                OfferEditRequestModel offer = new OfferEditRequestModel();

                offer.Price = Price;
                offer.Variant = Variant;
                offer.Description = Description;
                offer.Unit = SelectedUnit;
                offer.Product = productResult.Data;
                offer.Farm = farmResult.Data;
                offer.IsAvailable = isAvailable;
                offer.OfferImageUrl = ImageUrl ?? productResult.Data.ImageUrl;

                if (SelectedOffer is null)
                {
                    offer.Id = Guid.NewGuid().ToString();

                    var createResult = await _offerService.CreateAsync(offer, role);


                    if (createResult.IsSuccess)
                    {
                        await ToastHelper.ShowToastAsync($"New offer created for {offer.Product.Name}!");
                    }
                }
                else
                {
                    offer.Id = SelectedOffer.Id;
                    var updateResult = await _offerService.UpdateAsync(offer, role);

                    if (updateResult.IsSuccess)
                    {
                        await ToastHelper.ShowToastAsync($"Offer for {offer.Product.Name} updated!");
                    }
                }

                await Shell.Current.GoToAsync("..", true);

            });
    }
}
