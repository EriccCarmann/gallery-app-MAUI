using Microsoft.Maui.Controls.PlatformConfiguration;
using System;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels
{
    public class PhotoGridViewModel
    {
        private readonly IUnsplashService _unsplashService;
        public ObservableCollection<string> PhotoUrls { get; } = new ObservableCollection<string>();

        public PhotoGridViewModel(IUnsplashService unsplashService)
        {
            _unsplashService = unsplashService;
        }

        public async Task LoadRandomPhotosAsync(int num)
        {
            var photos = await _unsplashService.GetRandomPhotosAsync(num);
            PhotoUrls.Clear();

            foreach (var photo in photos)
            {
                var url = photo["urls"]?["small"]?.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    PhotoUrls.Add(url);
                    await Task.Delay(200);
                }
            }
        }

        public async Task LoadRandomLocalPhotosAsync(int num)
        {
            var photos = await _unsplashService.GetRandomPhotosAsync(num);
            PhotoUrls.Clear();

            foreach (var photo in photos)
            {
                var url = photo["urls"]?["small"]?.ToString();
                if (!string.IsNullOrEmpty(url))
                {
                    PhotoUrls.Add(url);
                    await Task.Delay(200);
                }
            }
        }
    }
}
