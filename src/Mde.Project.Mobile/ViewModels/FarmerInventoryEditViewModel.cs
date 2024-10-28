using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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

		public FarmerInventoryEditViewModel(IProductService productService, IOfferService offerService, IFarmService farmService)
		{
			_productService = productService;
			_offerService = offerService;
			_farmService = farmService;
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
				//selectedOffer = value; 
				SetProperty(ref selectedOffer, value);

				if (selectedOffer is not null)
				{
					PageTitle = "Edit offer";
					SelectedProduct = selectedOffer.Product;
					Description = selectedOffer.Description;
					Price = selectedOffer.Price;
					SelectedUnit = selectedOffer.Unit;
				}
				else
				{
					PageTitle = "Create new offer";
					SelectedProduct = default;
					Description = default;
					Price = default;
					SelectedUnit = default;
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

		private void LoadUnitOptions()
		{
			var units = Enum.GetValues(typeof(Unit)).Cast<Unit>();
            foreach (var unit in units)
            {
				UnitOptions.Add(unit);
            }
        }


		//public ICommand RefreshProductOptionsCommand =>
		//	new Command(async () =>
		//	{
		//		var result = await _productService.GetAllAsync();
		//		var products = result.Data;
		//		Products = new ObservableCollection<Product>(products);
		//	});

		public ICommand SaveCommand =>
			new Command(async () =>
			{
				var productResult = await _productService.GetByIdAsync(SelectedProduct.Id);
				var farmResult = await _farmService.GetByIdAsync(Guid.Parse("10000000-0000-0000-0000-000000000001"));

				OfferEditRequestModel offer = new OfferEditRequestModel();

				offer.Price = Price;
				offer.Description = Description;
				offer.Unit = SelectedUnit;
				offer.Product = productResult.Data.First();
				offer.Farm = farmResult.Data.First();

				if(SelectedOffer is null)
				{
					offer.Id = Guid.NewGuid();
					var createResult = await _offerService.CreateAsync(offer);

					if (createResult.IsSuccess)
					{
						await ToastHelper.ShowToastAsync($"New offer created for {offer.Product.Name}!");
						//IToast toast = Toast.Make($"New offer created for {offer.Product.Name}!");
						//await toast.Show();
					}
				}
				else
				{
					offer.Id = SelectedOffer.Id;
					var updateResult = await _offerService.UpdateAsync(offer);

					if (updateResult.IsSuccess)
					{
						await ToastHelper.ShowToastAsync($"Offer for {offer.Product.Name} updated!");

						//IToast toast = Toast.Make($"Offer for {offer.Product.Name} updated!");
						//await toast.Show();
					}
				}

				await Shell.Current.GoToAsync("..", true);

			});
	}
}
