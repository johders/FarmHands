using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserMapViewPage : ContentPage
{
    public UserMapViewPage(UserMapViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        UserMapViewModel viewModel = BindingContext as UserMapViewModel;
        viewModel.RefreshFarmMarkersCommand?.Execute(null);
        viewModel.GetUserLocationCommand?.Execute(null);
        base.OnAppearing();
    }
}