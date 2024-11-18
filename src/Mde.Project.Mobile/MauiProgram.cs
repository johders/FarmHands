﻿using CommunityToolkit.Maui;
using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Firestore;
using Mde.Project.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages.Farmer;
using Mde.Project.Mobile.Pages.Login;
using Mde.Project.Mobile.Pages.User;
using Mde.Project.Mobile.ViewModels;
using Microsoft.Extensions.Logging;

namespace Mde.Project.Mobile
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
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
            builder.Services.AddSingleton<IFirestoreContext, FirestoreContext>();

            builder.Services.AddTransient<IFarmService, FarmService>();

            builder.Services.AddTransient<IProductService, ProductMockService>();
            builder.Services.AddSingleton<IOfferService, OfferMockService>();

            builder.Services.AddSingleton<IFavoriteFarmService, FavoriteFarmMockService>();
            builder.Services.AddSingleton<IFavoriteProductService, FavoriteProductMockService>();
            builder.Services.AddSingleton<IUserPreferencesService, UserPreferencesMockService>();

            //builder.Services.AddSingleton<FarmService>();
            builder.Services.AddSingleton<ProductService>();
            builder.Services.AddSingleton<OfferService>();

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

            builder.Services.AddTransient<FarmerDashboardViewModel>();
            builder.Services.AddTransient<FarmerInventoryListViewModel>();
            builder.Services.AddTransient<FarmerInventoryEditViewModel>();
            builder.Services.AddTransient<FarmerSettingsViewModel>();

			builder.Services.AddTransient<LoginViewModel>();

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

            builder.Services.AddTransient<FarmerDashboardPage>();
            builder.Services.AddTransient<FarmerInventoryListPage>();
            builder.Services.AddTransient<FarmerInventoryEditPage>();
            builder.Services.AddTransient<FarmerSettingsPage>();

			builder.Services.AddTransient<LoginPage>();

			return builder;
        }
    }
}
