using GalleryApp.Models;
using GalleryApp.Services.Implementation;
using GalleryApp.ViewModels;

namespace GalleryApp.Views;

public partial class SavedPhotosPage : ContentPage
{
    private bool _isInitialized = false;
    private bool isLoading;
    private List<Photo> photos = new List<Photo>();
    private int currentPage = 1;

    public SavedPhotosPage()
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
                photos.AddRange(await viewModel.GetSavedPhotosAsync());
                foreach (var photo in photos)
                {
                    viewModel.Photos.Add(photo);
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
                var newPhotos = await viewModel.GetSavedPhotosAsync();
                foreach (var photo in newPhotos)
                {
                    if (!photos.Any(existing => existing.UrlSmall == photo.UrlSmall))
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
            DisplayAlert("Error", $"Failed to go to photo's Details: {ex.Message}", "OK");
        }
    }
}