using Mde.Project.Core.Services;
using Mde.Project.Core.Services.Interfaces;
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
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
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
            builder.Services.AddTransient<IFarmService, FarmMockService>();
            builder.Services.AddTransient<IProductService, ProductMockService>();

            return builder;
        }

        private static MauiAppBuilder RegisterViewModels(this MauiAppBuilder builder)
        {

            builder.Services.AddTransient<UserHomeViewModel>();

            return builder;
        }

        private static MauiAppBuilder RegisterViews(this MauiAppBuilder builder)
        {
            builder.Services.AddTransient<UserHomePage>();


            return builder;
        }
    }
}
