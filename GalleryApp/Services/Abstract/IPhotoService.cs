using GalleryApp.Models;
using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Abstract
{
    public interface IPhotoService
    {
        List<Photo> TurnIntoPhotoList(JArray photosJson);
    }
}
