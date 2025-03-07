using GalleryApp.Services.Abstract;
using GalleryApp.Services.Implementation;
using GalleryApp.ViewModels;
using GalleryApp.Views;
using Microsoft.Extensions.Logging;

namespace GalleryApp
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
                });

            builder.Services.AddSingleton<IUnsplashService, UnsplashService>();
            builder.Services.AddSingleton<IPhotoService, PhotoService>();

            builder.Services.AddSingleton<PhotoDetailsView>();

            builder.Services.AddSingleton<PhotoGridViewModel>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
