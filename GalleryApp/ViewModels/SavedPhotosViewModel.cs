using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalleryApp.Models;
using GalleryApp.Services.Abstract;
using System.Collections.ObjectModel;

namespace GalleryApp.ViewModels;

public class SavedPhotosViewModel : ObservableObject
{
    private readonly IPhotoService _photoService;
    private readonly INavigationService _navigationService;
    private readonly IDialogService _dialogService;
    private readonly HashSet<string> _loadedPhotoUrls = new HashSet<string>();

    private bool _isInitialized = false;
    private bool _isLoading = false;
    private int _currentPage = 1;

    public ObservableCollection<Photo> Photos { get; } = new ObservableCollection<Photo>();

    public IAsyncRelayCommand LoadPhotosCommand { get; }
    public IAsyncRelayCommand LoadMorePhotosCommand { get; }
    public IRelayCommand<Photo> OpenPhotoCommand { get; }

    public SavedPhotosViewModel(IPhotoService photoService, 
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
            var savedPhotos = await _photoService.GetSavedPhotosAsync();

            foreach (var photo in savedPhotos)
            {
                if (_loadedPhotoUrls.Add(photo.UrlRegular))
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
            var savedPhotos = await _photoService.GetSavedPhotosAsync();

            if (savedPhotos == null || !savedPhotos.Any()) return;

            foreach (var photo in savedPhotos)
            {
                if (_loadedPhotoUrls.Add(photo.UrlRegular))
                {
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
