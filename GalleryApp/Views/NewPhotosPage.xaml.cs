using GalleryApp.Models;
using GalleryApp.Services.Implementation;
using GalleryApp.ViewModels;

namespace GalleryApp.Views;

public partial class NewPhotosPage : ContentPage
{
    private bool _isInitialized = false;
    private bool isLoading;
    private List<Photo> photos = new List<Photo>();
    private int currentPage = 1;
    private HashSet<string> loadedPhotoUrls = new HashSet<string>();

    public NewPhotosPage()
	{
		InitializeComponent();
        
        BindingContext = new PhotoGridViewModel(new UnsplashService(), new PhotoService());
    }

    protected async override void OnAppearing()
    {
        base.OnAppearing();
        if (_isInitialized) return;

        try
        {
            if (BindingContext is PhotoGridViewModel viewModel)
            {
                var initialPhotos = await viewModel.GetRandomPhotosAsync(currentPage, 1);
                var savedPhotos = await viewModel.GetSavedPhotosAsync();

                foreach (var photo in initialPhotos)
                {
                    if (savedPhotos.Any(savedPhoto => savedPhoto.UrlSmall != photo.UrlSmall) &&
                        loadedPhotoUrls.Add(photo.UrlSmall))
                    {
                        viewModel.Photos.Add(photo);
                        photos.Add(photo);
                    }
                }

                _isInitialized = true;
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load photos: {ex.Message}", "OK");
        }
    }

    private async void RemainingItemsThresholdReached(object sender, EventArgs e)
    {
        if (isLoading) return;

        try
        {
            isLoading = true;

            currentPage++;

            if (BindingContext is PhotoGridViewModel viewModel)
            {
                var newPhotos = await viewModel.GetRandomPhotosAsync(currentPage, 15);
                var savedPhotos = await viewModel.GetSavedPhotosAsync();

                foreach (var photo in newPhotos)
                {
                    if (savedPhotos.Any(savedPhoto => savedPhoto.UrlSmall != photo.UrlSmall))
                    {
                        await Task.Delay(100);

                        viewModel.Photos.Add(photo);
                        photos.Add(photo);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load photos: {ex.Message}", "OK");
        }
        finally
        {
            isLoading = false;
        }
    }

    private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
    {
        try
        {
            var tappedImage = sender as Image;
            int imageIndex = 0;
            string imageUrl = tappedImage.BindingContext as string;

            if (tappedImage.Source is UriImageSource uriSource)
            {
                imageUrl = uriSource.Uri.ToString();
                foreach (var photo in photos)
                {
                    if (photo.UrlSmall == imageUrl)
                    {
                        imageIndex = photos.IndexOf(photo);
                        break;
                    }
                }
            }
            Navigation.PushAsync(new PhotoDetailsView(photos, imageIndex));
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Failed to go to photo's details: {ex.Message}", "OK");
        }
    }
}