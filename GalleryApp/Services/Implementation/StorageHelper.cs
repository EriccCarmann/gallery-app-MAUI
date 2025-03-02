using GalleryApp.Services.Abstract;
using Newtonsoft.Json.Linq;

namespace GalleryApp.Services.Implementation
{
    class StorageHelper : IStorageHelper
    {
        public async Task<FileResult> GetDownloadedPhotos()
        {
            string mainDir = FileSystem.Current.AppDataDirectory;
            string downloadDir = Path.Combine(mainDir, "Download");

            string[] imagePaths = Directory.GetFiles(downloadDir, "*.jpg");

            throw new NotImplementedException();
        }
    }
}
