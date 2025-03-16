using GalleryApp.Models;

namespace GalleryApp.Services.Abstract
{
    public interface INavigationService
    {
        void GoToPhotoDetails(List<Photo> photos, int index);
    }
}