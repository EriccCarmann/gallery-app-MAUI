using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using GalleryApp.Views;

namespace GalleryApp.Services.Implementation
{
    public class NavigationService : INavigationService
    {
        public void GoToPhotoDetails(List<Photo> photos, int index)
        {
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(new PhotoDetailsView(photos, index));
        }
    }
}
