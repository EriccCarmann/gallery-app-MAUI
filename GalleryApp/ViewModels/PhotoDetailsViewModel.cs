using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Data;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels
{
    public class PhotoDetailsViewModel : ObservableObject
    {
        private readonly IDialogService _dialogService;
        private readonly GalleryDatabase _galleryDatabase = new GalleryDatabase();


        public ObservableCollection<Photo> Photos { get; }
        public IRelayCommand<Photo> ToggleFavoriteCommand { get; }

        private int _currentPosition;
        public int CurrentPosition
        {
            get => _currentPosition;
            set => SetProperty(ref _currentPosition, value);
        }

        public PhotoDetailsViewModel(IDialogService dialogService,List<Photo> photos, int currentImageIndex)
        {
            _dialogService = dialogService;
            Photos = new ObservableCollection<Photo>(photos);
            CurrentPosition = currentImageIndex;

            ToggleFavoriteCommand = new RelayCommand<Photo>(OnToggleFavorite);
        }

        public async Task AddFavoriteImage()
        {
            try
            {
                var currentPhoto = Photos[CurrentPosition];

                var existing = await _galleryDatabase.GetByUrlAsync(currentPhoto.UrlSmall);
                if (existing != null)
                    return;

                var newPhoto = new Photo()
                {
                    UrlSmall = currentPhoto.UrlSmall,
                    UrlRegular = currentPhoto.UrlRegular,
                    Title = currentPhoto.Title,
                    Description = currentPhoto.Description,
                    IsFavorite = true
                };

                await _galleryDatabase.CreateAsync(newPhoto);
            }
            catch (Exception ex)
            {
                _dialogService.DisplayAlertMessage($"Failed to add favorite photo: {ex.Message}");
            }
        }

        public async Task DeleteFavoriteImage()
        {
            try
            {
                var currentPhoto = Photos[CurrentPosition];
                await _galleryDatabase.DeleteAsync(currentPhoto);
            }
            catch (Exception ex)
            {
                _dialogService.DisplayAlertMessage($"Failed to delete photo from favorites: {ex.Message}");
            }
        }

        private void OnToggleFavorite(Photo photo)
        {
            if (photo == null)
                return;

            if (!photo.IsFavorite)
            {
                photo.IsFavorite = true;
                _ = AddFavoriteImage();
            }
            else
            {
                photo.IsFavorite = false;
                _ = DeleteFavoriteImage();
            }
        }
    }
}
