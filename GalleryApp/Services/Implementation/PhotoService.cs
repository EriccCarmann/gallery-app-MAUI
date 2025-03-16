using GalleryApp.Data;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Implementation
{
    public class PhotoService : IPhotoService
    {
        private GalleryDatabase galleryDatabase = new GalleryDatabase();

        private readonly IUnsplashService _unsplashService;

        public PhotoService(IUnsplashService unsplashService)
        {
            _unsplashService = unsplashService;
        }

        public List<Photo> TurnIntoPhotoList(JArray photosJson)
        {
            var photos = new List<Photo>();

            try
            {
                foreach (var photo in photosJson)
                {
                    photos.Add(new Photo
                    {
                        UrlSmall = photo["urls"]?["small"]?.ToString() ?? "No Small Image",
                        UrlRegular = photo["urls"]?["regular"]?.ToString() ?? "No Regular Image",
                        Description = photo["description"]?.ToString() ?? "No Description",
                        Title = photo["alt_description"]?.ToString() ?? "No Title",
                        IsFavorite = false
                    });
                }

                return photos;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"JSON parsing error: {ex.Message}");
                throw;
            }
        }

        public async Task<List<Photo>> GetRandomPhotosAsync(int page, int per_page)
        {
            var photosJson = await _unsplashService.GetRandomPhotosAsync(page, per_page);
            return TurnIntoPhotoList(photosJson);
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
