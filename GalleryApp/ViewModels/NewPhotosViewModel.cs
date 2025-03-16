using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels
{
    public class NewPhotosViewModel : ObservableObject
    {
        private readonly IPhotoService _photoService;
        private readonly INavigationService _navigationService;
        private readonly IDialogService _dialogService;

        private bool _isInitialized = false;
        private bool _isLoading;
        private int _currentPage = 1;
        private HashSet<string> loadedPhotoUrls = new HashSet<string>();

        public ObservableCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();

        public IAsyncRelayCommand LoadPhotosCommand { get; }
        public IAsyncRelayCommand LoadMorePhotosCommand { get; }
        public IRelayCommand<Photo> OpenPhotoCommand { get; }


        public NewPhotosViewModel(IPhotoService photoService,
            INavigationService navigationService,
            IDialogService dialogService)
        {
            _photoService = photoService;
            _navigationService = navigationService;
            _dialogService = dialogService;

            LoadPhotosCommand = new AsyncRelayCommand(LoadPhotosAsync);
            LoadMorePhotosCommand = new AsyncRelayCommand(LoadMorePhotosAsync);
            OpenPhotoCommand = new RelayCommand<Photo>(OpenPhoto);
        }

        private async Task LoadPhotosAsync()
        {
            if (_isInitialized)
                return;

            try
            {
                var newPhotos = await _photoService.GetRandomPhotosAsync(_currentPage, 1);
                var savedPhotos = await _photoService.GetSavedPhotosAsync();

                foreach (var photo in newPhotos)
                {
                    if (!savedPhotos.Contains(photo) &&
                        loadedPhotoUrls.Add(photo.UrlSmall))
                    {
                        Photos.Add(photo);
                    }
                }
                _isInitialized = true;
            }
            catch (Exception ex)
            {
                _dialogService.DisplayAlertMessage($"Failed to load photos: {ex.Message}");
            }
        }

        private async Task LoadMorePhotosAsync()
        {
            if (_isLoading)
                return;

            _isLoading = true;
            try
            {
                _currentPage++;
                var newPhotos = await _photoService.GetRandomPhotosAsync(_currentPage, 15);
                var savedPhotos = await _photoService.GetSavedPhotosAsync();

                foreach (var photo in newPhotos)
                {
                    if (!savedPhotos.Contains(photo))
                    {
                        await Task.Delay(100);
                        Photos.Add(photo);
                    }
                }
            }
            catch (Exception ex)
            {
                _dialogService.DisplayAlertMessage($"Failed to load photos: {ex.Message}");
            }
            finally
            {
                _isLoading = false;
            }
        }

        private void OpenPhoto(Photo tappedPhoto)
        {
            _navigationService.GoToPhotoDetails(Photos.ToList(), Photos.IndexOf(tappedPhoto));
        }
    }
}
