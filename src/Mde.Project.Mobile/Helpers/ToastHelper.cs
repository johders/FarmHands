using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;

namespace Mde.Project.Mobile.Helpers
{
    public static class ToastHelper
    {
        public static async Task ShowToastAsync(string message, ToastDuration duration = ToastDuration.Short)
        {
            IToast toast = Toast.Make(message, duration);
            await toast.Show();
        }
    }
}
