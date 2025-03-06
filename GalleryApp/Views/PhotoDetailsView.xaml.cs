using GalleryApp.Data;
using GalleryApp.Models;

namespace GalleryApp.Views;

public partial class PhotoDetailsView : ContentPage
{
    private GalleryDatabase galleryDatabase = new GalleryDatabase();
    private List<Photo> _photos;
    int _currentImageIndex = 0;

    public PhotoDetailsView(List<Photo> photos, int currentImageIndex)
	{
		InitializeComponent();

        _photos = photos;
        _currentImageIndex = currentImageIndex;

        MyCarouselView.ItemsSource = _photos;
        MyCarouselView.Position = _currentImageIndex;
    }

    public async Task AddFavoriteImage() 
    {
        var currentPhoto = _photos[_currentImageIndex];

        var newPhoto = new Photo()
        {
            UrlSmall = currentPhoto.UrlSmall,
            UrlFull = currentPhoto.UrlFull,
            Title = currentPhoto.Title,
            Description = currentPhoto.Description,
            IsFavorite = true
        };

        await galleryDatabase.CreateAsync(newPhoto);
    }

    public async Task DeleteFavoriteImage()
    {
        await galleryDatabase.DeleteAsync(_photos[_currentImageIndex]);
    }

    public async Task PhotoIsSaved(Photo photo)
    {
        await galleryDatabase.GetAsync(photo.PhotoId);
    }

    void OnImageButtonClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;
        var currentPhoto = _photos[_currentImageIndex];

        if (button.Source.ToString().Contains("heart_empty"))
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
