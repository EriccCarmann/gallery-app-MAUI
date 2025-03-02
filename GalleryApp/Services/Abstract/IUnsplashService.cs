using Newtonsoft.Json.Linq;

namespace GalleryApp
{
    public interface IUnsplashService
    {
        Task<JArray> SearchPhotosAsync(string query);
        Task<JArray> GetRandomPhotosAsync(int count);
        Task<string> SavePhotoAsync(string image);
    }
}