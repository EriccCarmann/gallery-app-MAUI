using GalleryApp.Data;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels
{
    public class PhotoGridViewModel
    {
        private GalleryDatabase galleryDatabase = new GalleryDatabase();

        private readonly IUnsplashService _unsplashService;
        private readonly IPhotoService _photoService;

        private bool _isInitialized = false; 

        public ObservableCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();


        public PhotoGridViewModel(IUnsplashService unsplashService, IPhotoService photoService)
        {
            _unsplashService = unsplashService;
            _photoService = photoService;
        }

        public async Task<List<Photo>> GetRandomPhotosAsync(int num) 
        {
            var photosJson = await _unsplashService.GetRandomPhotosAsync(num);
            return _photoService.TurnIntoPhotoList(photosJson);
        }

        public async Task LoadRandomPhotosAsync(List<Photo> photos)
        {
            Photos.Clear();

            photos.AddRange(await galleryDatabase.GetAllAsync());

            foreach (var photo in photos)
            {
                var url = photo.UrlSmall;
                if (!string.IsNullOrEmpty(url))
                {
                    Photos.Add(photo);
                    await Task.Delay(200);
                }
            }
        }
    }
}
