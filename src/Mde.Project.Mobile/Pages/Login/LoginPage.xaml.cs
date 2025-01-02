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
                new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", "ya29.a0ARW5m76mgGX8Q3zWcUrSYIie0xy05T7TKcZKdhav5bs8B6ggCg08LNp8145tZ653zh79RALKSi5sS9nnQgUX2pr_SfSjNP_TN4EOUcTz5xOmquq63ICGXlNXRbYxzYXQcSNSdCs5xFl5UfptZioUQMtkW2-LKiEmEffnUiURaCgYKAVsSARMSFQHGX2MiHZx4T1SVFIqas98UNT70RA0175");

            string serializeRequest = JsonSerializer.Serialize(pushNotificationRequest);
            var response = await client.PostAsync(url, new StringContent(serializeRequest, Encoding.UTF8, "application/json"));

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                await App.Current.MainPage.DisplayAlert("notification sent", "notification sent", "OK");
            }
        }
    }
}