using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Implementation
{
    public class PhotoService : IPhotoService
    {
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
    }
}
