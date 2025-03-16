using GalleryApp.Data;
using GalleryApp.Models;

namespace GalleryApp.Views;

public partial class PhotoDetailsPage : ContentPage
{
    private GalleryDatabase galleryDatabase = new GalleryDatabase();
    private List<Photo> _photos;
    int _currentImageIndex = 0;

    public PhotoDetailsPage(List<Photo> photos, int currentImageIndex)
	{
		InitializeComponent();

        _photos = photos;
        _currentImageIndex = currentImageIndex;

        MyCarouselView.ItemsSource = _photos;
        MyCarouselView.Position = _currentImageIndex;
    }

    public async Task AddFavoriteImage() 
    {
        try
        {
            var currentPhoto = _photos[MyCarouselView.Position];

            var existing = await galleryDatabase.GetByUrlAsync(currentPhoto.UrlSmall);

            if (existing != null) return;

            var newPhoto = new Photo()
            {
                UrlSmall = currentPhoto.UrlSmall,
                UrlRegular = currentPhoto.UrlRegular,
                Title = currentPhoto.Title,
                Description = currentPhoto.Description,
                IsFavorite = true
            };

            await galleryDatabase.CreateAsync(newPhoto);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to add favorite photo: {ex.Message}", "OK");
        }
    }

    public async Task DeleteFavoriteImage()
    {
        try
        {
            var currentPhoto = _photos[MyCarouselView.Position];

            var existing = await galleryDatabase.GetByUrlAsync(currentPhoto.UrlSmall);

            await galleryDatabase.DeleteAsync(currentPhoto);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to delete photo from favorites: {ex.Message}", "OK");
        }
    }

    void OnImageButtonClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        var currentPhoto = _photos[MyCarouselView.Position];

        if (currentPhoto.IsFavorite != true)
        {
            currentPhoto.IsFavorite = true;
            AddFavoriteImage();
        }
        else
        {
            currentPhoto.IsFavorite = false;
            DeleteFavoriteImage();
        }
    }

}