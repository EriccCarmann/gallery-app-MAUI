using GalleryApp.ViewModels;
using GalleryApp.Models;

using GalleryApp.Services.Implementation;
using GalleryApp.Views;

namespace GalleryApp
{
    public partial class MainPage : ContentPage
    {
        private bool _isInitialized = false;
        private List<Photo> photos = new List<Photo>();

        public MainPage()
        {
            InitializeComponent();
            BindingContext = new PhotoGridViewModel(new UnsplashService(), new PhotoService());
        }

        protected async override void OnAppearing()
        {
            base.OnAppearing();

            if (_isInitialized) return;

            if (BindingContext is PhotoGridViewModel viewModel)
            {
                photos = await viewModel.GetRandomPhotosAsync(2);
                await viewModel.LoadRandomPhotosAsync(photos);

                _isInitialized = true;
            }
        }

        private void OnTapGestureRecognizerTapped(object sender, EventArgs e)
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
    }
}
