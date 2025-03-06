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
        var currentPhoto = _photos[MyCarouselView.Position];

        var existing = await galleryDatabase.GetByUrlAsync(currentPhoto.UrlSmall);

        if (existing != null) return;

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
        var currentPhoto = _photos[MyCarouselView.Position];

        var existing = await galleryDatabase.GetByUrlAsync(currentPhoto.UrlSmall);

        await galleryDatabase.DeleteAsync(currentPhoto);
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
