using GalleryApp.Data;
using GalleryApp.Models;
using static Microsoft.Maui.ApplicationModel.Permissions;

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

    protected override void OnAppearing()
    {
        base.OnAppearing();

        //MyCarouselView.ItemsSource = ;

        //foreach (Photo photo in _photos)
        //{
        //    if (PhotoIsSaved(photo) != null)
        //    {
        //        var currentPhoto = MyCarouselView.CurrentItem = photo;

        //        //"heart_empty.svg"

        //        //MyCarouselView.FavoriteImage
        //    }
        //}

        
    }

    void OnImageButtonClicked(object sender, EventArgs e)
    {
        var button = sender as ImageButton;

        if (button.Source.ToString().Contains("heart_empty"))
        {
            AddFavoriteImage();
            button.Source = "heart_full.svg";
        }
        else
        {
            DeleteFavoriteImage();
            button.Source = "heart_empty.svg";
        }
    }
}
