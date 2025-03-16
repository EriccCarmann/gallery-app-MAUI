using CommunityToolkit.Mvvm.ComponentModel;
using GalleryApp.Data;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels
{
    public class PhotoGridViewModel : ObservableObject
    {
        private GalleryDatabase galleryDatabase = new GalleryDatabase();

        private readonly IUnsplashService _unsplashService;
        private readonly IPhotoService _photoService;

        public ObservableCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();


        public PhotoGridViewModel(IUnsplashService unsplashService, IPhotoService photoService)
        {
            _unsplashService = unsplashService;
            _photoService = photoService;
        }

        public async Task<List<Photo>> GetRandomPhotosAsync(int page, int per_page) 
        {
            var photosJson = await _unsplashService.GetRandomPhotosAsync(page, per_page);
            return _photoService.TurnIntoPhotoList(photosJson);
        }

        public async Task<List<Photo>> GetSavedPhotosAsync()
        {
            try 
            {
                return await galleryDatabase.GetAllAsync();
            }
            catch (Exception ex) 
            {
                Console.WriteLine("Error", $"Failed to load photos: {ex.Message}", "OK");
                throw ex;
            }
        }
    }
}
