using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserPreferenceSettings : ContentPage
{
	private readonly int sliderIncrement = 1;
	private int sliderAdjustedValue;
	public UserPreferenceSettings()
	{
		InitializeComponent();
		BindingContext = new UserPreferencesViewModel();
	}

	private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		sliderAdjustedValue = (int)(e.NewValue / sliderIncrement) * sliderIncrement;
		lblDistance.Text = sliderAdjustedValue.ToString() + " km";
    }
}