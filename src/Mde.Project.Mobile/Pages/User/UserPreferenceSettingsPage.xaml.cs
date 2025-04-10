using Mde.Project.Mobile.ViewModels;

namespace Mde.Project.Mobile.Pages.User;

public partial class UserPreferenceSettingsPage : ContentPage
{
	private readonly int sliderIncrement = 1;
	private int sliderAdjustedValue;
	public UserPreferenceSettingsPage(UserPreferencesViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
		sliderAdjustedValue = 15;
	}

	protected override void OnAppearing()
	{
		lblDistance.Text = GetSliderLabelString(sliderAdjustedValue.ToString());
		base.OnAppearing();
	}

	private void Slider_ValueChanged(object sender, ValueChangedEventArgs e)
	{
		sliderAdjustedValue = (int)(e.NewValue / sliderIncrement) * sliderIncrement;
		lblDistance.Text = GetSliderLabelString(sliderAdjustedValue.ToString());
	}

	private void CheckBox_CheckedChanged(object sender, CheckedChangedEventArgs e)
	{
		if (BindingContext is UserPreferencesViewModel viewModel)
		{
			viewModel.UpdateSelectedCuisines();
		}
	}

	private string GetSliderLabelString(string sliderValue)
	{
		return sliderValue + " km";
	}
}