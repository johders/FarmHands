
namespace Mde.Project.Core.Services.Interfaces
{
    public interface IPushNotificationService
    {
        Task NotifyUsersAsync(string productId, string offerDescription);
        Task SendPushNotificationAsync(string deviceToken, string title, string body);
    }
}
