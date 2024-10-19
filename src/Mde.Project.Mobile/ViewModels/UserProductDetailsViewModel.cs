using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Core.Entities;

namespace Mde.Project.Mobile.ViewModels
{
	[QueryProperty(nameof(SelectedProduct), nameof(SelectedProduct))]
    public class UserProductDetailsViewModel : ObservableObject
    {
		private Product selectedProduct;

		public Product SelectedProduct
        {
			get { return selectedProduct; }
			set
			{
				SetProperty(ref selectedProduct, value);
			}
		}

	}
}
