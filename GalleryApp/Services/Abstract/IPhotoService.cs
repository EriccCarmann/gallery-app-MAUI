using GalleryApp.Models;
using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Abstract
{
    public interface IPhotoService
    {
        List<Photo> TurnIntoPhotoList(JArray photosJson);
        Task<List<Photo>> GetRandomPhotosAsync(int page, int per_page);
        Task<List<Photo>> GetSavedPhotosAsync();
    }
}
