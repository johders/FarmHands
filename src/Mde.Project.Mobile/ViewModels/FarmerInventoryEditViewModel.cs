using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services;
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

        private readonly ProductService _prodTesterService;

        public FarmerInventoryEditViewModel(IProductService productService, IOfferService offerService, IFarmService farmService, ProductService prodTesterService)
		{
			_productService = productService;
			_offerService = offerService;
			_farmService = farmService;

			_prodTesterService = prodTesterService;

			LoadUnitOptions();
			Products = new ObservableCollection<Product>(_productService.GetAll());
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

		public ICommand SaveCommand =>
			new Command(async () =>
			{
                //var productResult = await _productService.GetByIdAsync(SelectedProduct.Id);
                var productResult = await _prodTesterService.GetByIdAsync(SelectedProduct.Id);

                var farmResult = await _farmService.GetByIdAsync("10000000-0000-0000-0000-000000000007");

				OfferEditRequestModel offer = new OfferEditRequestModel();

				offer.Price = Price;
				offer.Variant = Variant;
				offer.Description = Description;
				offer.Unit = SelectedUnit;
				offer.Product = productResult.Data;
				offer.Farm = farmResult.Data;

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
