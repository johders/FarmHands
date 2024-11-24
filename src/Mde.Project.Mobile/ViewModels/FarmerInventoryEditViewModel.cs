using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Helpers;
using System.Collections.ObjectModel;
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

        public FarmerInventoryEditViewModel(IProductService productService, IOfferService offerService, IFarmService farmService, IFarmerService farmerService)
        {
            _productService = productService;
            _offerService = offerService;
            _farmService = farmService;

            LoadUnitOptions();
            Products = new ObservableCollection<Product>();
            _farmerService = farmerService;
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

		private Offer selectedOffer;
		public Offer SelectedOffer
		{
			get { return selectedOffer; }
			set 
			{
				SetProperty(ref selectedOffer, value);

				if (selectedOffer is not null)
				{
					PageTitle = "Edit offer";
					SelectedProduct = selectedOffer.Product;
					Variant = selectedOffer.Variant;
					Description = selectedOffer.Description;
					Price = selectedOffer.Price;
					SelectedUnit = selectedOffer.Unit;
					ImageUrl = selectedOffer.OfferImageUrl;
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


		private string description;
		public string Description
		{
			get { return description; }
			set { SetProperty(ref description, value); }
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
			set { SetProperty(ref price, value); }
		}

		private string imageUrl;
		public string ImageUrl
		{
			get { return imageUrl; }
			set { SetProperty(ref imageUrl, value); }
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
					var stream = await result.OpenReadAsync();
					var filePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);

					using (var fileStream = File.Create(filePath))
					{
						await stream.CopyToAsync(fileStream);
					}

					ImageUrl = filePath;
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
					var stream = await result.OpenReadAsync();
					var filePath = Path.Combine(FileSystem.AppDataDirectory, result.FileName);
					using (var fileStream = File.Create(filePath))
					{
						await stream.CopyToAsync(fileStream);
					}
					ImageUrl = filePath;
				}
			}
			catch (Exception ex)
			{
				var result = ex.Message;
                await Shell.Current.DisplayAlert("Error", $"Unable to take photo at this time, try again later: {ex.Message}", "OK");
            }
        });

		public ICommand SaveCommand =>
			new Command(async () =>
			{
                var productResult = await _productService.GetByIdAsync(SelectedProduct.Id);

				var uid = await SecureStorage.GetAsync("userId");
				var farmerResult = await _farmerService.GetFarmIdByFarmerAsync(uid);

				if (!farmerResult.IsSuccess)
				{
                    await Shell.Current.DisplayAlert("Oops", $"Unable to create offer at this time, try again later: {string.Join(", ", farmerResult.Errors)}", "OK");
					return;
                }

                var farmResult = await _farmService.GetByIdAsync(farmerResult.Data);

				OfferEditRequestModel offer = new OfferEditRequestModel();

				offer.Price = Price;
				offer.Variant = Variant;
				offer.Description = Description;
				offer.Unit = SelectedUnit;
				offer.Product = productResult.Data;
				offer.Farm = farmResult.Data;
				offer.OfferImageUrl = ImageUrl ?? productResult.Data.ImageUrl;

				if(SelectedOffer is null)
				{
					offer.Id = Guid.NewGuid().ToString();


                    var createResult = await _offerService.CreateAsync(offer);


                    if (createResult.IsSuccess)
					{
						await ToastHelper.ShowToastAsync($"New offer created for {offer.Product.Name}!");
					}
				}
				else
				{
					offer.Id = SelectedOffer.Id;
                    var updateResult = await _offerService.UpdateAsync(offer);

					if (updateResult.IsSuccess)
					{
						await ToastHelper.ShowToastAsync($"Offer for {offer.Product.Name} updated!");
					}
				}

				await Shell.Current.GoToAsync("..", true);

			});
	}
}
