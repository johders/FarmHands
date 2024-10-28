using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;
using Mde.Project.Core.Enums;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Core.Services.Models.RequestModels;
using Mde.Project.Mobile.Pages.Farmer;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
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
		}

		public ObservableCollection<Unit> UnitOptions { get; set; } = [];

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

		public ICommand RefreshProductOptionsCommand =>
			new Command(async () =>
			{
				var result = await _productService.GetAllAsync();
				var products = result.Data;
				Products = new ObservableCollection<Product>(products);
			});

		public ICommand SaveCommand =>
			new Command(async () =>
			{
				var productResult = await _productService.GetByIdAsync(SelectedProduct.Id);
				var farmResult = await _farmService.GetByIdAsync(Guid.Parse("10000000-0000-0000-0000-000000000001"));

				OfferCreateRequestModel offer = new OfferCreateRequestModel();

				offer.Id = Guid.NewGuid();
				offer.Price = price;
				offer.Description = description;
				offer.Unit = SelectedUnit;
				offer.Product = productResult.Data.First();
				offer.Farm = farmResult.Data.First();

				var createResult = await _offerService.CreateAsync(offer);

				if (createResult.IsSuccess)
				{
					await Shell.Current.GoToAsync(nameof(FarmerInventoryListPage), true);

					IToast toast = Toast.Make($"New offer created for {offer.Product.Name}!");
					await toast.Show();
				}
			});
	}
}
