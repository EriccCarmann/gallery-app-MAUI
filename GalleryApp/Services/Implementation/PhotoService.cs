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

            foreach (var photo in photosJson)
            {
                photos.Add(new Photo
                {
                    UrlSmall = photo["urls"]?["small"]?.ToString() ?? "No Small Image",
                    UrlFull = photo["urls"]?["full"]?.ToString() ?? "No Full Image",
                    Description = photo["description"]?.ToString() ?? "No Description",
                    Title = photo["alt_description"]?.ToString() ?? "No Title",
                    IsFavorite = false
                });
            }

            return photos;
        }
    }
}
