using Mde.Project.Mobile.Constants;
using Mde.Project.Mobile.Models;
using Mde.Project.Mobile.ViewModels;
using System.Text;
using System.Text.Json;

namespace Mde.Project.Mobile.Pages.Login;

public partial class LoginPage : ContentPage
{
    string _deviceToken;
    public LoginPage(LoginViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;

        if (Preferences.ContainsKey(AppConstants.DeviceToken))
        {
            _deviceToken = Preferences.Get(AppConstants.DeviceToken, "");
        }
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        LoginViewModel viewModel = BindingContext as LoginViewModel;

        await viewModel.CheckConnectionAsync();

        //NOTIFICATIONTEST
        var pushNotificationRequest = new PushNotificationRequest
        {
            Message = new MessageBody
            {
                Token = _deviceToken,
                Notification = new NotificationMessageBody
                {
                    Title = "Notification Title Buddy",
                    Body = "Push notification body, guy"
                }
            }
        };

        string url = "https://fcm.googleapis.com/v1/projects/farmhands-431df/messages:send";

        using (var client = new HttpClient())
        {
            client.DefaultRequestHeaders.Authorization = 
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "ya29.a0ARW5m75beUZlphl_hlydGy8fhFPweir5IlW2G1hz3n6JIFEAEtawaonr31Fj7iEnzHQI25ZTlQQ07CT14CVCO69nk0Gsj8WilQvvfk_IOByFIia7pDY-lWEKE6Yuls1Mkn6P8qAGIMn-Wv1_SlkKi46IyoKu-dqikNdmlcc7aCgYKAdYSARMSFQHGX2MijYQff5ruN8_xqrIfbRFUXw0175");

            string serializeRequest = JsonSerializer.Serialize(pushNotificationRequest);
            var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("notification sent", "notification sent", "OK");
            }
        }
    }
}