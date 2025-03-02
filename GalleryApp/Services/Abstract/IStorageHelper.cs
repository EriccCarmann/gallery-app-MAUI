using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Abstract
{
    public interface IStorageHelper
    {
        Task<FileResult> GetDownloadedPhotos();
    }
}
