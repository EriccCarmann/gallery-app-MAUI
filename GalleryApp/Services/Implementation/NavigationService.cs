using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using GalleryApp.ViewModels;
using GalleryApp.Views;

namespace GalleryApp.Services.Implementation
{
    public class NavigationService : INavigationService
    {
        private readonly IDialogService _dialogService;

        public NavigationService(IDialogService dialogService)
        {
            _dialogService = dialogService;
        }

        public void GoToPhotoDetails(List<Photo> photos, int index)
        {
            var navigation = Application.Current.MainPage.Navigation;
            navigation.PushAsync(new PhotoDetailsPage(
                new PhotoDetailsViewModel(_dialogService , photos, index)));
        }
    }
}
