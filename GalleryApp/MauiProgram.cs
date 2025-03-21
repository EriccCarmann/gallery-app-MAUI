﻿using GalleryApp.Services.Abstract;
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
            builder.Services.AddSingleton<IDialogService, DialogService>();
            builder.Services.AddSingleton<INavigationService, NavigationService>();
            
            builder.Services.AddSingleton<SavedPhotosViewModel>();
            builder.Services.AddSingleton<NewPhotosViewModel>();
            builder.Services.AddSingleton<PhotoDetailsViewModel>();

            builder.Services.AddSingleton<PhotoDetailsPage>();
            builder.Services.AddSingleton<SavedPhotosPage>();  
            builder.Services.AddSingleton<NewPhotosPage>();
#if DEBUG
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
