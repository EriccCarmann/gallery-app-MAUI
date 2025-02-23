using Newtonsoft.Json.Linq;

namespace GalleryApp
{
    public interface IUnsplashService
    {
        Task<JArray> SearchPhotosAsync(string query);
    }
}
