using CommunityToolkit.Maui;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Firestore;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using Mde.Project.Mobile.Pages.Login;
using Mde.Project.Mobile.Pages.User;
using Mde.Project.Mobile.Services;
using Mde.Project.Mobile.ViewModels;
using Microsoft.Extensions.Logging;
using Syncfusion.Maui.Core.Hosting;
using Mde.Project.Mobile.Constants;

namespace Mde.Project.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureSyncfusionCore()
                .UseMauiCommunityToolkit()
				.ConfigureFonts(fonts =>
                {
					fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
					fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemibold");
				})
                .RegisterServices()
                .RegisterViewModels()
                .RegisterViews();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }



        private static MauiAppBuilder RegisterServices(this MauiAppBuilder builder)
        {
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            builder.Services.AddSingleton<IConnectivityService, ConnectivityService>();

            builder.Services.AddSingleton<IFirestoreContext, FirestoreContext>();
            builder.Services.AddSingleton<IFirebaseAuthService, FirebaseAuthService>();
            builder.Services.AddSingleton<IAccountService, AccountService>();

            builder.Services.AddTransient<IFarmService, FarmService>();
            builder.Services.AddTransient<IProductService, ProductService>();
            builder.Services.AddSingleton<IOfferService, OfferService>();
            builder.Services.AddSingleton<IFavoriteFarmService, FavoriteFarmService>();
            builder.Services.AddSingleton<IFavoriteProductService, FavoriteProductService>();
            builder.Services.AddSingleton<IFarmerService, FarmerService>();
            builder.Services.AddSingleton<IUserPreferencesService, UserPreferencesMockService>();

            builder.Services.AddSingleton<IImageConversionService, ImageConversionService>();
            builder.Services.AddSingleton<IOpenStreetService, OpenStreetService>();
            builder.Services.AddTransient<IMealDbService, MealDbService>();
            builder.Services.AddSingleton<IPushNotificationService, PushNotificationService>();

            builder.Services.AddHttpClient(AppConstants.AddressClientName, client =>
            {
                client.BaseAddress = new Uri(AppConstants.StreetApiUrl);
                client.DefaultRequestHeaders.UserAgent.ParseAdd(AppConstants.UserAgentHeader);
            });

            builder.Services.AddHttpClient(AppConstants.MealClientName, client =>
            {
                client.BaseAddress = new Uri(AppConstants.MealApiUrl);
                client.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {

            builder.Services.AddTransient<UserHomeViewModel>();
            builder.Services.AddTransient<UserFarmDetailsViewModel>();
            builder.Services.AddTransient<UserProductDetailsViewModel>();
            builder.Services.AddTransient<UserFavoriteFarmsViewModel>();
            builder.Services.AddTransient<UserFavoriteProductsViewModel>();
            builder.Services.AddTransient<UserOfferDetailsViewModel>();
            builder.Services.AddTransient<UserAccountSettingsViewModel>();
            builder.Services.AddTransient<UserPreferencesViewModel>();
            builder.Services.AddTransient<UserMapViewModel>();

            builder.Services.AddTransient<FarmerDashboardViewModel>();
            builder.Services.AddTransient<FarmerInventoryListViewModel>();
            builder.Services.AddTransient<FarmerInventoryEditViewModel>();
            builder.Services.AddTransient<FarmerSettingsViewModel>();

			builder.Services.AddTransient<LoginViewModel>();
			builder.Services.AddTransient<RegisterViewModel>();
			builder.Services.AddTransient<RegisterOptionsViewModel>();

			return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<UserHomePage>();
            builder.Services.AddTransient<UserFarmDetailPage>();
            builder.Services.AddTransient<UserProductDetailPage>();
            builder.Services.AddTransient<UserFavoriteFarmsPage>();
            builder.Services.AddTransient<UserFavoriteProductsPage>();
            builder.Services.AddTransient<UserOfferDetailPage>();
            builder.Services.AddTransient<UserAccountSettingsPage>();
            builder.Services.AddTransient<UserPreferenceSettingsPage>();
            builder.Services.AddTransient<UserMapViewPage>();

            builder.Services.AddTransient<FarmerDashboardPage>();
            builder.Services.AddTransient<FarmerInventoryListPage>();
            builder.Services.AddTransient<FarmerInventoryEditPage>();
            builder.Services.AddTransient<FarmerSettingsPage>();

			builder.Services.AddTransient<LoginPage>();
			builder.Services.AddTransient<RegisterPage>();
			builder.Services.AddTransient<RegisterOptionsPage>();

			return builder;
        }
    }
}
